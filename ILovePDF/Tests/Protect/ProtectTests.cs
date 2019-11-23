using System;
using System.Security.Authentication;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Protect
{
    [TestClass]
    public class ProtectTests : BaseTest
    {
        public ProtectTests()
        {
            TaskParams = new ProtectParams(Settings.RightPassword)
            {
                OutputFileName = @"result.pdf"
            };
        }

        private new ProtectParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<ProtectTask>(null, true)
                    : Api.CreateTask<ProtectTask>();
            else
                Task = Api.CreateTask<ProtectTask>(TaskParams.FileEncryptionKey);

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
        public void Protect_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void Protect_WrongFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.BadPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Protect_UploadFileFromServer_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodPdfUrl)});

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void Protect_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException),
            "OutputFileName bigger than allowed was inappropriately processed.")]
        public void Protect_BigFileName_ShouldThrowException()
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
        public void Protect_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Protect_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Mistaken Password was inappropriately processed.")]
        public void Protect_WrongPassword_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.WrongPassword);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Protect_RightPassword_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.RightPassword);

            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Protect_ProvidingPackageName_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 5; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.PackageFileName = @"package";
            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Protect_DefaultParams_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsTrue(RunTask());
        }
    }
}