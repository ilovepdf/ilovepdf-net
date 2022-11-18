using LovePdf.Core;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Edit;
using LovePdf.Model.TaskParams.Sign.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Authentication;

namespace Tests.Edit
{
    [TestClass]
    public class SignTests : BaseTest
    {
        public SignTests()
        {
            TaskParams = new SignParams(); 
            TaskParams.OutputFileName = @"result.pdf";
        }

        private new SignParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            CreateApiTask(encryptUsingBuiltinIfNoKeyPresent);

            base.TaskParams = TaskParams;

            var taskWasOk = AddFilesToTask(addFilesByChunks);

            if (taskWasOk)
                taskWasOk = ProcessTask();

            if (taskWasOk)
                taskWasOk = DownloadResult(downloadFileAsByteArray);

            return taskWasOk;
        }

        protected void CreateApiTask(Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (!IsTaskSetted)
            {
                if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                    Task = encryptUsingBuiltinIfNoKeyPresent
                        ? Api.CreateTask<SignTask>(null, true)
                        : Api.CreateTask<SignTask>();
                else
                    Task = Api.CreateTask<SignTask>(TaskParams.FileEncryptionKey);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException),
            "A user with invalid credentials should not be allowed, but it was")]
        public void Sign_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void Sign_WrongFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.BadPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void Sign_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Sign_BigOutputFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.OutputFileName = Arrange_BigOutputFileName();

            AssertThrowsException_BigOutputFileName(() => RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Encryption Key was inappropriately processed.")]
        public void Sign_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Sign_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Mistaken Password was inappropriately processed.")]
        public void Sign_WrongPassword_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.WrongPassword);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Sign_RightPassword_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.RightPassword);

            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Sign_ProvidingPackageName_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < 5; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.PackageFileName = @"package";
            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Elements cannot be blank.")]
        public void Sign_DefaultParams_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Elements cannot be blank.")]
        public void Sign_2()
        {
            var api = new LovePdfApi("project_public_c0ac272c966a051c024a9efcd05e0837_lGE_M521c52f2c0421da14164986b6a281270",
              "secret_key_7f1d69768ded72ab078cd21f555088b3_PUd5_0ecef4507c55c30a28c22bd42ce4f408");

            // Create sign task
            var task = api.CreateTask<SignTask>();

            // File variable contains server file name
            var file = task.AddFile(@"C:\Users\Conqueror\source\repos\ilovepdf-net-fork\tests\UnitTests\Data\should-work.pdf");

            // Create task params
            var signParams = new SignParams();

            // Create a signer
            var signer = signParams.AddSigner("Signer", "abdurahim.khudoyberdiev@gmail.com");

            // Add file that a receiver of type signer needs to sign.
            var signerFile = signer.AddFile(file.ServerFileName);

            // Add signers and their elements;
            var signatureElement = signerFile.AddSignature();
            signatureElement.Position = new Position(20, -20);
            signatureElement.Pages = "1";
            signatureElement.Size = 40;

            // Lastly send the signature request
            var signature = task.RequestSignatureAsync(signParams).Result;
        }

    }
}