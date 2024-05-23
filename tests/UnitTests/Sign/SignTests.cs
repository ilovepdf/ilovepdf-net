using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Sign;
using LovePdf.Model.TaskParams.Sign.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

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
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void Sign_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
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
        public async Task Sign_ComplexTest_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            CreateApiTask(false);

            // File variable contains server file name
            var file = Task.AddFile($"{Settings.DataPath}{Path.DirectorySeparatorChar}{Settings.GoodPdfFile}");

            // Create task params
            var signParams = new SignParams();
            signParams.ExpirationDays = 10;

            // Create a signer
            var signerEmail = "signer@example.com";
            var signer = signParams.AddSigner("Signer", signerEmail);

            // Add file that a receiver of type signer needs to sign.
            var signerFile = signer.AddFile(file.ServerFileName);

            // Add signers and their elements;
            var signatureElement = signerFile.AddSignature();
            signatureElement.Position = new Position(200, -20);
            signatureElement.Pages = "1";
            signatureElement.Size = 40;

            var validator = signParams.AddValidator("Validator", "validator@example.com");

            // Lastly send the signature request
            var signature = await (Task as SignTask).RequestSignatureAsync(signParams);

            var response = await (Task as SignTask).GetSignatureStatusAsync(signature.TokenRequester);
            Assert.AreEqual("sent", response.Status);
             
            var increaseResonse = await (Task as SignTask).IncreaseExpirationDaysAsync(signature.TokenRequester, 10);
            Assert.AreEqual(Convert.ToDateTime(signature.Expires).AddDays(10).Date,  Convert.ToDateTime(increaseResonse.Expires).Date);

            var downloadedFile = await (Task as SignTask).DownloadOriginalFilesAsync(signature.TokenRequester, "./");
            Assert.IsTrue(File.Exists(downloadedFile));

            var signatures = await (Task as SignTask).GetSignaturesAsync(new ListRequest(0, 100));
            Assert.IsTrue(signatures.Count > 0);
        }

        [TestMethod]
        public async Task Sign_RequestSignature_ShouldProccessOk()
        {
            InitApiWithRightCredentials();

            CreateApiTask(false);

            // File variable contains server file name
            var file = Task.AddFile($"{Settings.DataPath}{Path.DirectorySeparatorChar}{Settings.GoodPdfFile}");

            // Create task params
            var signParams = new SignParams();

            // Create a signer
            var signerEmail = "signer@example.com";
            var signer = signParams.AddSigner("Signer", signerEmail);

            // Add file that a receiver of type signer needs to sign.
            var signerFile = signer.AddFile(file.ServerFileName);

            // Add signers and their elements;
            var signatureElement = signerFile.AddSignature();
            signatureElement.Position = new Position(200, -20);
            signatureElement.Pages = "1";
            signatureElement.Size = 40;

            // Lastly send the signature request
            var signature = await (Task as SignTask).RequestSignatureAsync(signParams);
            Assert.AreEqual(signerEmail, signature.Signers.First().Email);
        } 
    }
}