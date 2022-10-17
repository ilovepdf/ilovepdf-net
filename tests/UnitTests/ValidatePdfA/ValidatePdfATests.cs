using System;
using System.Security.Authentication;
using LovePdf.Model.Enums;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ValidatePdfA
{
    [TestClass]
    public class ValidatePdfATests : BaseTest
    {
        public ValidatePdfATests()
        {
            TaskParams = new ValidatePdfAParams
            {
                OutputFileName = @"result.pdf"
            };
        }

        private new ValidatePdfAParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<ValidatePdfATask>(null, true)
                    : Api.CreateTask<ValidatePdfATask>();
            else
                Task = Api.CreateTask<ValidatePdfATask>(TaskParams.FileEncryptionKey);

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
        public void ValidatePdfA_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ValidatePdfA_NonConformant_ShouldValidateFileAsNonConformant()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.Conformance = ConformanceValues.PdfA3A;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ValidatePdfA_NonConformant_UploadFileFromServer_ShouldValidateFileAsNonConformant()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodPdfUrl)});

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void ValidatePdfA_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException),
            "OutputFileName bigger than allowed was inappropriately processed.")]
        public void ValidatePdfA_BigFileName_ShouldThrowException()
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
        public void ValidatePdfA_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ValidatePdfA_NonConformant_ProvidingEncryptKey_ShouldValidateFileAsNonConformant()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ValidatePdfA_NonConformant_RightPassword_ShouldValidateFileAsNonConformant()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.RightPassword);

            TaskParams.IgnoreErrors = false;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ValidatePdfA_NonConformant_ProvidingPackageName_ShouldValidateFileAsNonConformant()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 5; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.PackageFileName = @"package";
            TaskParams.IgnoreErrors = false;

            Assert.IsFalse(RunTask());
        }
    }
}