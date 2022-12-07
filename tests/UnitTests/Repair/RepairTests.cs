using System;
using System.Security.Authentication;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Repair
{
    [TestClass]
    public class RepairTests : BaseTest
    {
        public RepairTests()
        {
            TaskParams = new RepairParams
            {
                OutputFileName = @"result.pdf"
            };
        }

        private new RepairParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<RepairTask>(null, true)
                    : Api.CreateTask<RepairTask>();
            else
                Task = Api.CreateTask<RepairTask>(TaskParams.FileEncryptionKey);

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
        public void Repair_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void Repair_WrongFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.BadPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Repair_UploadFileFromServer_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodPdfUrl)});

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void Repair_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Repair_BigOutputFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.OutputFileName = Arrange_BigOutputFileName();

            AssertThrowsException_BigOutputFileName(() => RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Encryption Key was inappropriately processed.")]
        public void Repair_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Repair_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Mistaken Password was inappropriately processed.")]
        public void Repair_WrongPassword_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.WrongPassword);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Repair_RightPassword_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.RightPassword);

            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Repair_ProvidingPackageName_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 5; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.PackageFileName = @"package";
            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Repair_DefaultParams_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodMultipagePdfFile);

            Assert.IsTrue(RunTask());
        }
    }
}