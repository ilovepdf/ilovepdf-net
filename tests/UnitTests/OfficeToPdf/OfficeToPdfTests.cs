using System;
using System.Security.Authentication;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.OfficeToPdf
{
    [TestClass]
    public class OfficeToPdfTests : BaseTest
    {
        public OfficeToPdfTests()
        {
            TaskParams = new OfficeToPdfParams
            {
                OutputFileName = @"result.pdf"
            };
        }

        private new OfficeToPdfParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<OfficeToPdfTask>(null, true)
                    : Api.CreateTask<OfficeToPdfTask>();
            else
                Task = Api.CreateTask<OfficeToPdfTask>(TaskParams.FileEncryptionKey);

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
        public void OfficeToPdf_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.doc", Settings.GoodWordFile);

            Assert.IsFalse(RunTask());
        }

        //Comentado porque esta request no devuelve una excepcion
        //[TestMethod]
        //[ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        //public void OfficeToPdf_WrongWordFile_ShouldThrowException()
        //{
        //    InitApiWithRightCredentials();

        //    AddFile($"{Guid.NewGuid()}.doc", Settings.BadWordFile);

        //    Assert.IsFalse(RunTask());
        //}

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void OfficeToPdf_WrongExcelFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.xlsx", Settings.BadExcelFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void OfficeToPdf_WordFile_DefaultParams_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.doc", Settings.GoodWordFile);

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void OfficeToPdf_ExcelFile_DefaultParams_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.xlsx", Settings.GoodExcelFile);

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void OfficeToPdf_UploadFileFromServer_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodWordUrl)});

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void OfficeToPdf_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.doc", Settings.GoodWordFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void OfficeToPdf_BigOutputFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.doc", Settings.GoodWordFile);

            TaskParams.OutputFileName = Arrange_BigOutputFileName();

            AssertThrowsException_BigOutputFileName(() => RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Encryption Key was inappropriately processed.")]
        public void OfficeToPdf_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.doc", Settings.GoodWordFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void OfficeToPdf_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.doc", Settings.GoodWordFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void OfficeToPdf_ProvidingPackageName_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.doc", Settings.GoodWordFile);

            AddFile($"{Guid.NewGuid()}.doc", Settings.GoodWordFile);

            AddFile($"{Guid.NewGuid()}.xlsx", Settings.GoodExcelFile);

            TaskParams.PackageFileName = @"package";
            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }
    }
}