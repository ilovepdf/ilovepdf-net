using System;
using System.Security.Authentication;
using iLovePdf.Model.Enums;
using iLovePdf.Model.Exception;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.HtmlToPdf;

namespace Tests.HtmlToPdf
{
    [TestClass]
    public class HtmlToPdfTests : BaseTest
    {
        public HtmlToPdfTests()
        {
            TaskParams = new HTMLtoPDFParams
            {
                OutputFileName = @"result.pdf", 
            };
        }

        private new HTMLtoPDFParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<HtmlToPdfTask>(null, true)
                    : Api.CreateTask<HtmlToPdfTask>();
            else
                Task = Api.CreateTask<HtmlToPdfTask>(TaskParams.FileEncryptionKey);

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
        public void HtmlToPdf_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodHtmlUrl) });

            Assert.IsFalse(RunTask());
        }

        //Comentado porque esta request ya no devuelve excepcion
        //[TestMethod]
        //[ExpectedException(typeof(ServerErrorException), "A wrong file url wath passed.")]
        //public void HtmlToPdf_WrongFileUri_ShouldThrowException()
        //{
        //    InitApiWithRightCredentials();

        //    AddFile(new UriForTest { FileUri = new Uri(Settings.BadHtmlUrl) });

        //    Assert.IsFalse(RunTask());
        //}

        [TestMethod]
        public void HtmlToPdf_UploadFileFromServer_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodHtmlUrl) });

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void HtmlToPdf_BigOutputFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.OutputFileName = Arrange_BigOutputFileName();

            AssertThrowsException_BigOutputFileName(() => RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Delay param was inappropriately processed.")]
        public void HtmlToPdf_WrongDelay_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodHtmlUrl) });

            TaskParams.Delay = 10000;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Encryption Key was inappropriately processed.")]
        public void HtmlToPdf_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodHtmlUrl) });

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void HtmlToPdf_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

             AddFile(new UriForTest {FileUri = new Uri(Settings.GoodHtmlUrl) });

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void HtmlToPdf_ProvidingPackageName_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 5; i++)
                 AddFile(new UriForTest {FileUri = new Uri(Settings.GoodHtmlUrl) });

            TaskParams.PackageFileName = @"package";
            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void HtmlToPdf_CorrectParams_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

             AddFile(new UriForTest {FileUri = new Uri(Settings.GoodHtmlUrl) });

            TaskParams.Delay = 2;

            Assert.IsTrue(RunTask());
        }
    }
}
