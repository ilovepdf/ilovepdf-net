using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Tests
{
    public abstract partial class BaseTest
    {
        protected BaseTest()
        {
            Files = new ();
        }

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        protected TestContext TestContext { get; set; }

        protected iLovePdfApi Api { get; private set; }

        protected iLovePdfTask Task {  get; set; }

        protected Boolean IsTaskSetted => Task != null;

        protected BaseParams TaskParams { private get; set; }

        private List<FileItem> Files { get; }

        public class  FileItem 
        {
            public BaseElementForTest FileObject { get; set; }
            public Action<string> Callback { get; set; }

        }

        protected void InitApiWithWrongCredentials()
        {
            Api = new iLovePdfApi(Settings.WrongPublicKey, Settings.WrongSecretKey);
        }

        protected void InitApiWithRightCredentials()
        {
            Api = new iLovePdfApi(Settings.RightPublicKey, Settings.RightSecretKey);
        }

        protected Boolean ProcessTask()
        {
            var taskResult = Task.Process(TaskParams);

            if (taskResult.Validations.Any(v => v.Status == ValidationStatus.NonConformant))
                return false;

            var startTime = DateTime.Now;
            while (Task.CheckTaskStatus(Task.TaskId).Status == "Running" ||
                   Task.CheckTaskStatus(Task.TaskId).Status == "TaskWaiting")
            {
                if ((DateTime.Now - startTime).TotalSeconds > Settings.TimeoutSeconds)
                    return false;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            var taskStatusResponse = Task.CheckTaskStatus(Task.TaskId);

            if (taskStatusResponse.Status != "TaskSuccess") 
            {
                throw new Exception(JsonConvert.SerializeObject(taskStatusResponse, Formatting.Indented));
            }

            return taskStatusResponse.Status == "TaskSuccess";
        }

        protected Boolean AddFilesToTask(Boolean addFilesByChunks)
        {
            try
            {
                foreach (FileItem file in Files)
                {
                    AddFileToTask(file, addFilesByChunks);
                } 
            }
            catch
            {
                throw;
            }

            return true;
        }

        public UploadTaskResponse AddFileToTask(FileItem file, Boolean addFilesByChunks)
        {
            UploadTaskResponse response = null;
             
            switch (file.FileObject)
            {
                case FileForTest fileForTest when addFilesByChunks:
                    response = Task.AddFileByChunks(fileForTest.FileName, Task.TaskId, fileForTest.Password,fileForTest.Rotation);
                    break;

                case FileForTest fileForTest:
                    response = Task.AddFile(fileForTest.FileName, Task.TaskId, fileForTest.Password, fileForTest.Rotation);
                    break;

                case UriForTest uriForTest:
                    response = Task.AddFile(uriForTest.FileUri, Task.TaskId, uriForTest.Password, uriForTest.Rotation);
                    break;
            }

            if (response != null && file.Callback != null) 
            {
                file.Callback(response.ServerFileName);
            }

            return response;
        }
          
        protected Boolean DownloadResult(Boolean downloadFileAsByteArray)
        {
            String resultFile;

            var resultShouldBeMultipage = false;

            if (Task.ToolName == EnumExtensions.GetEnumDescription(TaskName.Split))
            {
                var splitParams = (SplitParams) TaskParams;
                if (splitParams.SplitMode == SplitModes.FixedRange || splitParams.SplitMode == SplitModes.Ranges)
                    if (splitParams.MergeAfter != true)
                        resultShouldBeMultipage = true;
            }

            if (Files.Count > 1 && 
                Task.ToolName != EnumExtensions.GetEnumDescription(TaskName.Merge) &&
                Task.ToolName != EnumExtensions.GetEnumDescription(TaskName.Edit) &&
                Task.ToolName != EnumExtensions.GetEnumDescription(TaskName.WaterMark))
                resultShouldBeMultipage = true;

            if (resultShouldBeMultipage)
                resultFile = TaskParams.PackageFileName != null
                    ? $"{Settings.BasePath}{Path.DirectorySeparatorChar}{TaskParams.PackageFileName}.zip"
                    : $"{Settings.BasePath}{Path.DirectorySeparatorChar}{Settings.DefaultMultipageOutput}";
            else
                resultFile = TaskParams.OutputFileName != null
                    ? $"{Settings.BasePath}{Path.DirectorySeparatorChar}{TaskParams.OutputFileName}"
                    : $"{Settings.BasePath}{Path.DirectorySeparatorChar}{Settings.DefaultSinglepageOutput}";

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
                File.WriteAllBytes($"{Settings.BasePath}{Path.DirectorySeparatorChar}{TaskParams.OutputFileName}",
                    fileAsByteArray);
            }
            else
            {
                Task.DownloadFile(Settings.BasePath, Task.TaskId);
            }

            if (!File.Exists(resultFile))
                return false;

            File.Delete(resultFile);

            Task.DeleteTask(Task.TaskId);

            return true;
        }

        protected void AddFile(UriForTest originalFileUri, Action<string> callback = null)
        {
            Files.Add(new FileItem() { 
                Callback = callback,
                FileObject = originalFileUri
            });
        }

        protected void AddFile(String addedFileName, String originalFileName, iLovePdf.Model.Enums.Rotate rotation,
            String password = null, Action<string> callback = null)
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

            Files.Add(new FileItem()
            {
                Callback = callback,
                FileObject = file
            });
        }

        protected void AddFile(String addedFileName, String originalFileName, String password = null, Action<string> callback = null)
        {
            File.Copy($"{Settings.DataPath}{Path.DirectorySeparatorChar}{originalFileName}",
                $"{Settings.DataPath}{Path.DirectorySeparatorChar}{addedFileName}");

            var file = new FileForTest
            {
                FileName = $"{Settings.DataPath}{Path.DirectorySeparatorChar}{addedFileName}"
            };

            if (password != null)
                file.Password = password;

            Files.Add(new FileItem()
            {
                Callback = callback,
                FileObject = file
            });
        }

        private void cleanUsedData()
        {
            foreach (var file in Files.Where(f => f is FileForTest))
            {
                var fileForTest = (FileForTest)file.FileObject;
                if (File.Exists(fileForTest.FileName))
                    File.Delete(fileForTest.FileName);
            }
        }

        protected Boolean RunTask()
        {
            Boolean taskWasOk;

            try
            {
                taskWasOk = DoRunTask(false, false, false);

                if (taskWasOk)
                {
                    DoRunTask(false, false, true);
                    DoRunTask(false, true, false);
                    DoRunTask(false, true, true);

                    if (Files.All(f => f is FileForTest))
                    {
                        DoRunTask(true, false, false);
                        DoRunTask(true, false, true);
                        DoRunTask(true, true, false);
                        DoRunTask(true, true, true);
                    }
                }

                cleanUsedData();
            }
            catch (Exception)
            {
                cleanUsedData();
                throw;
            }

            return taskWasOk;
        }

        protected abstract Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent);
    }
}