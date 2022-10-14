using System;
using System.Security.Authentication;
using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Edit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Edit
{
    [TestClass]
    public class EditTests : BaseTest
    {
        public EditTests()
        {
            TaskParams = new EditParams
            {
                OutputFileName = @"result.pdf"
            };
        }

        private new EditParams TaskParams { get; }

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
                        ? Api.CreateTask<EditTask>(null, true)
                        : Api.CreateTask<EditTask>();
                else
                    Task = Api.CreateTask<EditTask>(TaskParams.FileEncryptionKey);
            }           
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException),
            "A user with invalid credentials should not be allowed, but it was")]
        public void Edit_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void Edit_WrongFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.BadPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Edit_UploadFileFromServer_AndAddTextElement_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest { FileUri = new Uri(Settings.GoodPdfUrl) });

            TaskParams.AddElement(new TextElement()
            {
                Text = "TestText",
            });

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Edit_UploadFileFromServer_AndAddImageElement_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest { FileUri = new Uri(Settings.GoodPdfUrl) });

            CreateApiTask(false);
            var upload = AddFileToTask(new UriForTest { FileUri = new Uri(Settings.GoodJpgUrl) }, false);
            
            TaskParams.AddElement(new ImageElement(upload.ServerFileName)
            { 
                Coordinates = new Coordinate(100, 100),
                Dimensions = new Dimension(200, 200)
            });
            
            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void Edit_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException),
            "OutputFileName bigger than allowed was inappropriately processed.")]
        public void Edit_BigFileName_ShouldThrowException()
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
        public void Edit_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Edit_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Mistaken Password was inappropriately processed.")]
        public void Edit_WrongPassword_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.WrongPassword);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void Edit_RightPassword_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFilePasswordProtected, Settings.RightPassword);

            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Edit_ProvidingPackageName_ShouldProcessOk()
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
        public void Edit_DefaultParams_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsTrue(RunTask());
        }
    }
}