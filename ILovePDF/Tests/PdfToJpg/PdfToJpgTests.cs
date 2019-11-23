using System;
using System.Security.Authentication;
using LovePdf.Model.Enums;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.PdfToJpg
{
    [TestClass]
    public class PdfToJpgTests : BaseTest
    {
        public PdfToJpgTests()
        {
            TaskParams = new PdftoJpgParams
            {
                OutputFileName = @"result.jpg",
                PdfJpgMode = PdfToJpgModes.Pages
            };
        }

        private new PdftoJpgParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<PdfToJpgTask>(null, true)
                    : Api.CreateTask<PdfToJpgTask>();
            else
                Task = Api.CreateTask<PdfToJpgTask>(TaskParams.FileEncryptionKey);

            base.TaskParams = TaskParams;

            var taskWasOk = AddFilesToTask(addFilesByChunks);

            if (taskWasOk)
                taskWasOk = ProcessTask();

            if (taskWasOk)
                taskWasOk = DownloadResult(downloadFileAsByteArray);

            return taskWasOk;
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException),
            "A user with invalid credentials should not be allowed, but it was")]
        public void PdfToJpg_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void PdfToJpg_WrongFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.BadPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void PdfToJpg_UploadFileFromServer_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodPdfUrl)});

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void PdfToJpg_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException),
            "OutputFileName bigger than allowed was inappropriately processed.")]
        public void PdfToJpg_BigFileName_ShouldThrowException()
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
        [ExpectedException(typeof(ProcessingException), "Mistaken mode was inappropriately processed.")]
        public void PdfToJpg_WrongMode_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.PdfJpgMode = (PdfToJpgModes) 255;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Encryption Key was inappropriately processed.")]
        public void PdfToJpg_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void PdfToJpg_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Mistaken Password was inappropriately processed.")]
        public void PdfToJpg_WrongPassword_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.WrongPassword);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void PdfToJpg_RightPassword_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.RightPassword);

            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void PdfToJpg_ProvidingPackageName_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 5; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.PackageFileName = @"package";
            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void PdfToJpg_CorrectParams_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.PdfJpgMode = PdfToJpgModes.Pages;

            Assert.IsTrue(RunTask());
        }
    }
}