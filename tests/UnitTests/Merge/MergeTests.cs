using System;
using System.Security.Authentication;
using iLovePdf.Model.Exception;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Merge
{
    [TestClass]
    public class MergeTests : BaseTest
    {
        public MergeTests()
        {
            TaskParams = new MergeParams
            {
                OutputFileName = @"result.pdf"
            };
        }

        private new MergeParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<MergeTask>(null, true)
                    : Api.CreateTask<MergeTask>();
            else
                Task = Api.CreateTask<MergeTask>(TaskParams.FileEncryptionKey);

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
        public void Merge_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            for (var i = 0; i < 2; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void Merge_WrongPdfFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 2; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.BadPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Only one file was inappropriately processed.")]
        public void Merge_OnlyOneFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Merge_UploadFileFromServer_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 2; i++)
                AddFile(new UriForTest {FileUri = new Uri(Settings.GoodPdfUrl)});

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void Merge_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Merge_BigOutputFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 2; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.OutputFileName = Arrange_BigOutputFileName();

            AssertThrowsException_BigOutputFileName(() => RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Encryption Key was inappropriately processed.")]
        public void Merge_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 2; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Merge_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 2; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Merge_DefaultParams_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 2; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }
    }
}