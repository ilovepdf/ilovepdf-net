using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.Exception;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.WaterMark
{
    [TestClass]
    public class WaterMarkTests : BaseTest
    {
        private bool uploadWaterMarkFile; 
        public WaterMarkTests()
        {
            TaskParams = new WaterMarkParams(new WatermarkModeText(Settings.WaterMarkText))
            {
                OutputFileName = @"result.pdf"
            };
        }

        private new WaterMarkParams TaskParams { get; set; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            CreateApiTask(encryptUsingBuiltinIfNoKeyPresent);

            var taskWasOk = AddFilesToTask(addFilesByChunks);

            base.TaskParams = TaskParams;

            if (uploadWaterMarkFile)
                UploadWatermarkImage();

            if (taskWasOk)
                taskWasOk = ProcessTask();

            if (taskWasOk)
                taskWasOk = DownloadResult(downloadFileAsByteArray);

            return taskWasOk;
        }

        private void CreateApiTask(bool encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<WaterMarkTask>(null, true)
                    : Api.CreateTask<WaterMarkTask>();
            else
                Task = Api.CreateTask<WaterMarkTask>(TaskParams.FileEncryptionKey);
        }

        public UploadTaskResponse UploadWatermarkImage()
        {
            var waterMarkFile = new WaterMarkTask().UploadWatermark($"{Settings.DataPath}\\{Settings.GoodPngFile}", 
                Task.TaskId, Task.ServerUrl, 0);

            TaskParams = new WaterMarkParams(new WatermarkModeImage(waterMarkFile.ServerFileName));
            TaskParams.Mode = WaterMarkModes.Image;
            TaskParams.Image = waterMarkFile.ServerFileName;
            TaskParams.OutputFileName = @"result.pdf";

            return waterMarkFile;
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException),
            "A user with invalid credentials should not be allowed, but it was")]
        public void WaterMark_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void WaterMark_WrongFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.BadPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void WaterMark_UploadFileFromServer_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest { FileUri = new Uri(Settings.GoodPdfUrl) });

            Assert.IsTrue(RunTask());
        }

        //[TestMethod]
        //[ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        //public void WaterMark_MaxFilesAdded_ShouldThrowException()
        //{
        //    InitApiWithRightCredentials();

        //    for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
        //        AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

        //    Assert.IsFalse(RunTask());
        //}

        [TestMethod]
        public void WaterMark_BigOutputFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.OutputFileName = Arrange_BigOutputFileName();

            AssertThrowsException_BigOutputFileName(() => RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Encryption Key was inappropriately processed.")]
        public void WaterMark_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void WaterMark_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Mistaken Password was inappropriately processed.")]
        public void WaterMark_WrongPassword_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.WrongPassword);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Mistaken WaterMark Mode was inappropriately processed.")]
        public void WaterMark_WrongWaterMarkMode_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.WrongPassword);

            TaskParams.Mode = (WaterMarkModes)255;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void WaterMark_RightPassword_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.RightPassword);

            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        //[TestMethod]
        //public void WaterMark_ProvidingPackageName_ShouldProcessOk()
        //{
        //    InitApiWithRightCredentials();

        //    for (var i = 0; i < 5; i++)
        //        AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

        //    TaskParams.PackageFileName = @"package";
        //    TaskParams.IgnoreErrors = false;

        //    Assert.IsTrue(RunTask());
        //}

        [TestMethod]
        public void WaterMark_TextWaterMark_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.Mode = WaterMarkModes.Text;
            TaskParams.Text = Settings.WaterMarkText;

            Assert.IsTrue(RunTask());
        }

        //[TestMethod]
        //public void WaterMark_ImageWaterMark_ShouldProcessOk()
        //{
        //    InitApiWithRightCredentials();

        //    AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

        //    uploadWaterMarkFile = true;

        //    Assert.IsTrue(RunTask());
        //}

        //Comentado porque no se soporta lista de watermarks
        //[TestMethod]
        //public void WaterMark_MultiWaterMark_ShouldProcessOk()
        //{
        //    InitApiWithRightCredentials();

        //    AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

        //    AddFile(new UriForTest { FileUri = new Uri(Settings.GoodJpgUrl) }, serverFileName =>
        //    {
        //        var elements = new List<WaterMarkParamsElement>()
        //        {
        //            new WaterMarkParamsElement(new WatermarkModeImage(serverFileName)),
        //            new WaterMarkParamsElement(new WatermarkModeText(Settings.WaterMarkText))
        //        };

        //        TaskParams = new WaterMarkParams(elements);
        //        TaskParams.Mode = WaterMarkModes.Multi;
        //        TaskParams.OutputFileName = @"result.pdf";
        //    });

        //    Assert.IsTrue(RunTask());
        //}
    }
}