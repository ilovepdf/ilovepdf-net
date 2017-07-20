using LovePdf.Model.Enums;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using LovePdf.Core;

namespace Tests
{
    public abstract class BaseTest
    {

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        protected TestContext TestContext { get; set; }

        protected LovePdfApi Api { get; private set; }

        protected LovePdfTask Task { private get; set; }

        protected BaseParams TaskParams { private get; set; }

        private List<object> Files { get; }

        protected BaseTest()
        {
            Files = new List<object>();
        }

        protected void InitApiWithWrongCredentials()
        {
            Api = new LovePdfApi(Settings.WrongPublicKey, Settings.WrongSecretKey);
        }

        protected void InitApiWithRightCredentials()
        {
            Api = new LovePdfApi(Settings.RightPublicKey, Settings.RightSecretKey);
        }

        protected bool ProcessTask()
        {
            var taskResult = Task.Process(TaskParams);

            if (taskResult.Validations.Any(v => v.Status == ValidationStatus.NonConformant))
                return false;

            var startTime = DateTime.Now;
            while (Task.CheckTaskStatus(Task.TaskId).TaskStatus == "Running" ||
                   Task.CheckTaskStatus(Task.TaskId).TaskStatus == "TaskWaiting")
            {
                if ((DateTime.Now - startTime).TotalSeconds > Settings.TimeoutSeconds)
                    return false;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            var taskStatusResponse = Task.CheckTaskStatus(Task.TaskId);

            return taskStatusResponse.TaskStatus == "TaskSuccess";
        }

        protected bool AddFilesToTask(bool addFilesByChunks)
        {
            foreach (var file in Files)
            {
                if (file is FileForTest)
                {
                    var fileForTest = file as FileForTest;

                    if (addFilesByChunks)
                        Task.AddFileByChunks(fileForTest.FileName, Task.TaskId, fileForTest.Password, fileForTest.Rotation);
                    else
                        Task.AddFile(fileForTest.FileName, Task.TaskId, fileForTest.Password, fileForTest.Rotation);
                }
                else if (file is UriForTest)
                {
                    var uriForTest = file as UriForTest;
                    Task.AddFile(uriForTest.FileUri, Task.TaskId, uriForTest.Password, uriForTest.Rotation);
                }
            }

            return true;
        }

        protected bool DownloadResult(bool downloadFileAsByteArray)
        {
            string resultFile;

            var resultShouldBeMultipage = false;

            if (Task.ToolName == EnumExtensions.GetEnumDescription(TaskName.Split))
            {
                var splitParams = (SplitParams)TaskParams;
                if (splitParams.SplitMode == SplitModes.FixedRange || splitParams.SplitMode == SplitModes.Ranges)
                    if (splitParams.MergeAfter != true)
                        resultShouldBeMultipage = true;
            }

            if (Files.Count > 1 && Task.ToolName != EnumExtensions.GetEnumDescription(TaskName.Merge))
                resultShouldBeMultipage = true;

            if (resultShouldBeMultipage)
            {
                resultFile = TaskParams.PackageFileName != null ?
                    $"{Settings.BasePath}{Path.DirectorySeparatorChar}{TaskParams.PackageFileName}.zip" :
                    $"{Settings.BasePath}{Path.DirectorySeparatorChar}{Settings.DefaultMultipageOutput}";
            }
            else
            {
                resultFile = TaskParams.OutputFileName != null ?
                    $"{Settings.BasePath}{Path.DirectorySeparatorChar}{TaskParams.OutputFileName}" :
                    $"{Settings.BasePath}{Path.DirectorySeparatorChar}{Settings.DefaultSinglepageOutput}";
            }

            if (Task.ToolName == EnumExtensions.GetEnumDescription(TaskName.PdfToJpg))
                if (TaskParams.OutputFileName != null)
                    resultFile = TaskParams.PackageFileName != null
                        ? $"{Settings.BasePath}{Path.DirectorySeparatorChar}{TaskParams.PackageFileName}.zip"
                        : $"{Settings.BasePath}{Path.DirectorySeparatorChar}{TaskParams.OutputFileName.Replace("result", "result-0001")}";

            if (File.Exists(resultFile))
                File.Delete(resultFile);

            if (downloadFileAsByteArray)
            {
                var fileAsByteArray = Task.DownloadFileAsByteArrayAsync(Task.TaskId).Result;
                File.WriteAllBytes($"{Settings.BasePath}{Path.DirectorySeparatorChar}{TaskParams.OutputFileName}", fileAsByteArray);
            }
            else
                Task.DownloadFile(Settings.BasePath, Task.TaskId);

            if (!File.Exists(resultFile))
                return false;

            File.Delete(resultFile);

            Task.DeleteFile(Task.TaskId);

            Task.DeleteTask(Task.TaskId);

            return true;
        }

        protected void AddFile(UriForTest originalFileUri)
        {
            Files.Add(originalFileUri);
        }

        protected void AddFile(string addedFileName, string originalFileName, LovePdf.Model.Enums.Rotate rotation, string password = null)
        {
            File.Copy($"{Settings.DataPath}{Path.DirectorySeparatorChar}{originalFileName}",
                $"{Settings.DataPath}{Path.DirectorySeparatorChar}{addedFileName}");

            var file = new FileForTest
            {
                FileName = $"{Settings.DataPath}{Path.DirectorySeparatorChar}{addedFileName}"
            };

            if (password != null)
                file.Password = password;

            file.Rotation = rotation;

            Files.Add(file);
        }

        protected void AddFile(string addedFileName, string originalFileName, string password = null)
        {
            File.Copy($"{Settings.DataPath}{Path.DirectorySeparatorChar}{originalFileName}",
                $"{Settings.DataPath}{Path.DirectorySeparatorChar}{addedFileName}");

            var file = new FileForTest
            {
                FileName = $"{Settings.DataPath}{Path.DirectorySeparatorChar}{addedFileName}"
            };

            if (password != null)
                file.Password = password;

            Files.Add(file);
        }

        private void CleanUsedData()
        {
            foreach (var file in Files.Where(f => f is FileForTest))
            {
                var fileForTest = (FileForTest)file;
                if (File.Exists(fileForTest.FileName))
                    File.Delete(fileForTest.FileName);
            }
        }

        protected bool RunTask()
        {
            bool taskWasOk;

            try
            {

                taskWasOk = DoRunTask(false, false, false);

                if (taskWasOk) DoRunTask(false, false, true);

                if (taskWasOk) DoRunTask(false, true, false);

                if (taskWasOk) DoRunTask(false, true, true);

                if (Files.All(f => f is FileForTest))
                {
                    if (taskWasOk) DoRunTask(true, false, false);

                    if (taskWasOk) DoRunTask(true, false, true);

                    if (taskWasOk) DoRunTask(true, true, false);

                    if (taskWasOk) DoRunTask(true, true, true);
                }

                CleanUsedData();

            }
            catch (Exception)
            {
                CleanUsedData();
                throw;
            }

            return taskWasOk;
        }

        protected abstract bool DoRunTask(
            bool addFilesByChunks,
            bool downloadFileAsByteArray,
            bool encryptUsingBuiltinIfNoKeyPresent);

    }
}