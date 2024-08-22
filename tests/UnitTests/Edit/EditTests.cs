using LovePdf.Model.Exception;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Edit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Authentication;

namespace Tests.Edit
{
    [TestClass]
    public class EditTests : BaseTest
    {
        public EditTests()
        {  
            TaskParams = new EditParams();
        }

        private new EditParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            CreateApiTask(encryptUsingBuiltinIfNoKeyPresent);
                     
            var taskWasOk = AddFilesToTask(addFilesByChunks);

            base.TaskParams = TaskParams;

            if (taskWasOk)
                taskWasOk = ProcessTask();

            if (taskWasOk)
                taskWasOk = DownloadResult(downloadFileAsByteArray);

            return taskWasOk;
        }

        protected void CreateApiTask(Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<EditTask>(null, true)
                    : Api.CreateTask<EditTask>();
            else
                Task = Api.CreateTask<EditTask>(TaskParams.FileEncryptionKey);
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

            TaskParams.OutputFileName = @"result.pdf";
            TaskParams.Clear();
            TaskParams.AddText("Text for test");
                        
            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void Edit_UploadFileFromServer_AndAddImageElement_ShouldProcessOk()
        {
            TaskParams.Clear();
            TaskParams.OutputFileName = @"result.pdf";

            InitApiWithRightCredentials();
          
            AddFile(new UriForTest { FileUri = new Uri(Settings.GoodPdfUrl) });

            AddFile(new UriForTest { FileUri = new Uri(Settings.GoodJpgUrl) }, serverFileName => {
                TaskParams.Elements.Clear();
                var image = TaskParams.AddImage(serverFileName);
                image.Dimensions = new Dimension(200, 200);
                image.Coordinates = new Coordinate(1, 1);               
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
        public void Edit_BigOutputFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            TaskParams.OutputFileName = Arrange_BigOutputFileName();

            AssertThrowsException_BigOutputFileName(() => RunTask());
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
        [ExpectedException(typeof(ProcessingException), "Elements cannot be blank.")]
        public void Edit_DefaultParams_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.pdf", Settings.GoodPdfFile);

            Assert.IsTrue(RunTask());
        }
    }
}