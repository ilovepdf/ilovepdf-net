using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Add watermark to PDFs
    /// </summary>
    public class WaterMarkTask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.WaterMark);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(WaterMarkParams parameters)
        {
            if (parameters == null)
                throw new ArgumentException("Parameters should not be null", nameof(parameters));

            return base.Process(parameters);
        }

        /// <summary>
        ///     Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="UriFile"></param>
        /// <returns>Server file name</returns>
        [SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse UploadWatermarkFromUrl(Uri UriFile)
        {
            return UploadWatermarkFromUrl(UriFile, TaskId, ServerUrl, Rotate.Degrees0);
        }

        /// <summary>
        ///     Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="UriFile"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="serverUrl"></param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        [SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse UploadWatermarkFromUrl(Uri UriFile, String taskId, Uri serverUrl, Rotate rotate)
        {
            if (UriFile == null)
                throw new ArgumentException("cannot be null", nameof(UriFile));

            var requestTaskId = String.IsNullOrWhiteSpace(taskId) ? TaskId : taskId;

            var response = RequestHelper.Instance.UploadFile(serverUrl, UriFile, requestTaskId);

            //TODO check filename
            var fileName = Path.GetFileName(UriFile.AbsoluteUri);

            if (string.IsNullOrEmpty(fileName))
            {
                var host = UriFile.Host;
                if (host.Contains('.'))
                {
                    var parts = UriFile.Host.Split('.');
                    fileName = parts[parts.Length - 2];
                }
                else
                {
                    fileName = host;
                }
            }

            return response;
        }
        /// <summary>
        ///     Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Server file name</returns>
        [SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse UploadWatermark(string path)
        {
            return UploadWatermark(path, TaskId, ServerUrl, Rotate.Degrees0);
        }

        /// <summary>
        ///     Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="serverUrl"></param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        [SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse UploadWatermark(string path, string taskId, Uri serverUrl, Rotate rotate)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) throw new FileNotFoundException("File not found", fileInfo.FullName);

            var response = RequestHelper.Instance.UploadFile(serverUrl, fileInfo, taskId);

            return response;
        }

    }
}