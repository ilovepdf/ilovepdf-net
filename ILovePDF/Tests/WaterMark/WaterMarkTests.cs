using System;
using System.Security.Authentication;
using LovePdf.Model.Enums;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.WaterMark
{
    [TestClass]
    public class WaterMarkTests : BaseTest
    {

        private new WaterMarkParams TaskParams { get; }

        public WaterMarkTests()
        {
            TaskParams = new WaterMarkParams(new WatermarkModeText(Settings.WaterMarkText))
            {
                OutputFileName = @"result.pdf"
            };
        }

        protected override bool DoRunTask(
            bool addFilesByChunks,
            bool downloadFileAsByteArray,
            bool encryptUsingBuiltinIfNoKeyPresent)
        {
            if (string.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<WaterMarkTask>(null, true)
                    : Api.CreateTask<WaterMarkTask>();
            else
                Task = Api.CreateTask<WaterMarkTask>(TaskParams.FileEncryptionKey);

            base.TaskParams = TaskParams;

            var taskWasOk = AddFilesToTask(addFilesByChunks);

            if (taskWasOk)
                taskWasOk = ProcessTask();

            if (taskWasOk)
                taskWasOk = DownloadResult(downloadFileAsByteArray);

            return taskWasOk;
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException), "A user with invalid credentials should not be allowed, but it was")]
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

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void WaterMark_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "OutputFileName bigger than allowed was inappropriately processed.")]
        public void WaterMark_BigFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            var outputFileName = @"";
            for (var i = 0; i < Settings.MaxCharactersInFilename; i++)
                outputFileName = $"{outputFileName}a";
            TaskParams.OutputFileName = $"{outputFileName}.pdf";

            Assert.IsFalse(RunTask());
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

        [TestMethod]
        public void WaterMark_ProvidingPackageName_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 5; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.PackageFileName = @"package";
            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void WaterMark_TextWaterMark_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.Mode = WaterMarkModes.Text;
            TaskParams.Text = Settings.WaterMarkText;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void WaterMark_ImageWaterMark_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.Mode = WaterMarkModes.Image;
            TaskParams.Image = Settings.GoodJpgUrl;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void WaterMark_MultiWaterMark_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.Mode = WaterMarkModes.Multi;
            TaskParams.Elements.Add(new WaterMarkParamsElement(
                new WatermarkModeImage(Settings.GoodJpgUrl)));
            TaskParams.Elements.Add(new WaterMarkParamsElement(
                new WatermarkModeText(Settings.WaterMarkText)));

            Assert.IsTrue(RunTask());
        }
    }
}
