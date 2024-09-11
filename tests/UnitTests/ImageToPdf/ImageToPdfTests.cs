using System;
using System.Security.Authentication;
using iLovePdf.Model.Enums;
using iLovePdf.Model.Exception;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ImageToPdf
{
    [TestClass]
    public class OfficeToPdfTests : BaseTest
    {
        public OfficeToPdfTests()
        {
            TaskParams = new ImageToPdfParams
            {
                OutputFileName = @"result.pdf"
            };
        }

        private new ImageToPdfParams TaskParams { get; }

        protected override Boolean DoRunTask(
            Boolean addFilesByChunks,
            Boolean downloadFileAsByteArray,
            Boolean encryptUsingBuiltinIfNoKeyPresent)
        {
            if (String.IsNullOrWhiteSpace(TaskParams.FileEncryptionKey))
                Task = encryptUsingBuiltinIfNoKeyPresent
                    ? Api.CreateTask<ImageToPdfTask>(null, true)
                    : Api.CreateTask<ImageToPdfTask>();
            else
                Task = Api.CreateTask<ImageToPdfTask>(TaskParams.FileEncryptionKey);

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
        public void ImageToPdf_WrongCredentials_ShouldThrowException()
        {
            InitApiWithWrongCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void ImageToPdf_WrongJpgFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.BadJpgFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void ImageToPdf_WrongPngFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.png", Settings.BadPngFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "A Damaged File should was inappropriately processed.")]
        public void ImageToPdf_WrongTiffFile_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.tiff", Settings.BadTiffFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ImageToPdf_DefaultParams_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        public void ImageToPdf_UploadFileFromServer_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile(new UriForTest {FileUri = new Uri(Settings.GoodJpgUrl)});

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(UploadException), "More files than allowed were inappropriately processed.")]
        public void ImageToPdf_MaxFilesAdded_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            for (var i = 0; i < Settings.MaxAllowedFiLes; i++)
                AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ImageToPdf_BigOutputFileName_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            TaskParams.OutputFileName = Arrange_BigOutputFileName();

            AssertThrowsException_BigOutputFileName(() => RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong Encryption Key was inappropriately processed.")]
        public void ImageToPdf_WrongEncryptionKey_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            TaskParams.FileEncryptionKey = Settings.WrongEncryptionKey;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ImageToPdf_ProvidingEncryptKey_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            TaskParams.IgnoreErrors = false;
            TaskParams.FileEncryptionKey = Settings.RightEncryptionKey;

            Assert.IsTrue(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Wrong orientation was inappropriately processed.")]
        public void ImageToPdf_WrongOrientation_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            TaskParams.Orientation = (Orientations) 2;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        [ExpectedException(typeof(ProcessingException), "Wrong page size was inappropriately processed.")]
        public void ImageToPdf_WrongPageSize_ShouldThrowException()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            TaskParams.PageSize = (PageSizes) 3;

            Assert.IsFalse(RunTask());
        }

        [TestMethod]
        public void ImageToPdf_ProvidingPackageName_ShouldProcessOk()
        {
            InitApiWithRightCredentials();

            AddFile($"{Guid.NewGuid()}.jpg", Settings.GoodJpgFile);

            AddFile($"{Guid.NewGuid()}.png", Settings.GoodPngFile);

            AddFile($"{Guid.NewGuid()}.tiff", Settings.GoodTiffFile);

            TaskParams.PackageFileName = @"package";
            TaskParams.MergeAfter = false;
            TaskParams.IgnoreErrors = false;

            Assert.IsTrue(RunTask());
        }
    }
}