using System;
using System.IO;
using System.Net.Http;
using ILovePDF.Core.Model;
using ILovePDF.Core.TaskRequest;
using ILovePDF.Core.TaskResponse;
using System.Collections.Generic;
using System.Linq;
using ILovePDF.Model.TaskParams;
using System.Threading.Tasks;
using ILovePDF.Model.Enum;

namespace ILovePDF.Model.Task
{
    public class FileModel
    {

        public string ServerFileName { get; set; }
        public string FileName { get; set; }
        public Rotate Rotate { get; set; }
        public string Password { get; set; }
    }

    public abstract class LovePdfTask
    {
        public string ServerUrl { get; set; }
        public string TaskId { get; set; }

        private List<FileModel> Files { get; set; }

        /// <summary>
        /// Get current running tool name
        /// </summary>
        /// <returns></returns>
        public virtual string GetToolName()
        {
            return string.Empty;
        }
        /// <summary>
        /// Set server url and task id
        /// </summary>
        /// <param name="serverUrl">server url from Create task response</param>
        /// <param name="taskId">Task id from Create task response</param>
        public void SetServerTaskId(string serverUrl, string taskId)
        {
            ServerUrl = serverUrl;
            TaskId = taskId;

            Files = new List<FileModel>();
        }

        /// <summary>
        /// Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="password"></param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFile(string path, string taskId = null, string password = null, Rotate rotate = Rotate._0)
        {
            var requestedTaskId = string.IsNullOrWhiteSpace(taskId) ? TaskId : taskId;
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File not found", fileInfo.FullName);
            }

            var response = RequestHelper.Instance.UploadFile(ServerUrl, fileInfo, requestedTaskId);

            Files.Add(new FileModel() { ServerFileName = response.ServerFileName, FileName = fileInfo.Name, Password = password, Rotate = rotate });

            return response;
        }

        /// <summary>
        /// Upload file to the ILovePdf server by chunks
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="passwrod"></param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFileByChunks(string path, string taskId = null, string passwrod = null, Rotate rotate = Rotate._0)
        {
            var requestedTaskId = string.IsNullOrWhiteSpace(taskId) ? TaskId : taskId;
            var fileInfo = new FileInfo(path);

            //Throw exception if file not found
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File not found", fileInfo.FullName);
            }

            var response = RequestHelper.Instance.UploadFileByChunk(ServerUrl, fileInfo, requestedTaskId);

            Files.Add(new FileModel { ServerFileName = response.ServerFileName, FileName = fileInfo.Name, Password = passwrod, Rotate = rotate });

            return response;
        }

        /// <summary>
        /// Upload file to the ILovePdf server as byte array.
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="fileName">specify file name with extension otherwise file name will be the same as task id.</param>
        /// <param name="password"></param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFile(byte[] fileBytes, string taskId = null, string fileName = null, string password = null, Rotate rotate = Rotate._0)
        {
            string requestedTaskId = string.IsNullOrWhiteSpace(taskId) ? taskId : TaskId;
            string originalFileName = string.IsNullOrWhiteSpace(fileName) ? requestedTaskId : fileName;

            var response = RequestHelper.Instance.UploadFile(ServerUrl, fileBytes, originalFileName, requestedTaskId);

            Files.Add(new FileModel { ServerFileName = response.ServerFileName, FileName = originalFileName, Password = password, Rotate = rotate });

            return response;
        }

        /// <summary>
        /// Upload file to the ILovePdf server from the url.
        /// </summary>
        /// <param name="cloudLink"></param>
        /// <param name="taskId"></param>
        /// <param name="password">protect file with password</param>
        /// <param name="rotate">rotate file on: 0,90,180,20</param>
        /// <returns>Server file name.</returns>
        public UploadTaskResponse AddFile(Uri cloudLink, string taskId = null, string password = null, Rotate rotate = Rotate._0)
        {
            string requestTaskId = string.IsNullOrWhiteSpace(taskId) ? TaskId : taskId;

            var response = RequestHelper.Instance.UploadFile(ServerUrl, cloudLink, requestTaskId);

            //TODO check filename
            var fileName = Path.GetFileName(cloudLink.AbsoluteUri);
            Files.Add(new FileModel { ServerFileName = response.ServerFileName, FileName = fileName, Password = password, Rotate = rotate });

            return response;
        }

        /// <summary>
        /// Execute current task
        /// </summary>
        protected string Process(BaseParams parameters)
        {
            var executeResult = RequestHelper.Instance
                 .ExecuteTask(ServerUrl, TaskId, Files, GetToolName(), parameters);

            //Remove files after each task execute. To prevent processing same files.
            Files.Clear();

            return executeResult;
        }

        /// <summary>
        /// Download output files(s) from ILovePDF server to the specific location.
        /// </summary>
        /// <param name="destination">path where file will be stored</param>
        /// <param name="task">task id</param>
        public void DownloadFile(string destination, string task = null)
        {
            string requestedTask = string.IsNullOrWhiteSpace(task) ? TaskId : task;

            RequestHelper.Instance.Download(this.ServerUrl, requestedTask, destination);
        }

        /// <summary>
        /// Download output file(s) from ILovePDF server as byte array
        /// </summary>
        /// <param name="task"></param>
        /// <returns>by</returns>
        public Task<byte[]> DownloadFileAsByteArrayAsync(string task = null)
        {
            string requestedTask = string.IsNullOrWhiteSpace(task) ? TaskId : task;

            return RequestHelper.Instance.DownloadAsync(this.ServerUrl, requestedTask);
        }

        /// <summary>
        /// Check task status. If no task passed will be checked status of the last one.
        /// </summary>
        /// <param name="task">task to check.</param>
        /// <returns></returns>
        public StatusTaskResponse CheckTaskStatus(string task = null)
        {
            string requestedTaskId = string.IsNullOrWhiteSpace(task) ? TaskId : task;

            return RequestHelper.Instance.CheckTaskStatus(ServerUrl, requestedTaskId);
        }

        /// <summary>
        /// Delete specific task. If no task passed wil be deleted the last one.
        /// </summary>
        /// <param name="task">task to delete.</param>
        /// <returns></returns>
        public DeleteTaskResponse DeleteTask(string task = null)
        {
            var requestedTaskId = string.IsNullOrWhiteSpace(task) ? TaskId : task;

            return RequestHelper.Instance.DeleteTask(ServerUrl, requestedTaskId);
        }

        /// <summary>
        /// Delete file from server.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="serverFileName">file name on server</param>
        public void DeleteFile(string serverFileName, string task = null)
        {
            if (string.IsNullOrWhiteSpace(serverFileName))
            {
                throw new ArgumentNullException(nameof(serverFileName), "Specify server file name");
            }
            string requestedTaskId = string.IsNullOrWhiteSpace(task) ? TaskId : task;

            RequestHelper.Instance.DeleteFile(ServerUrl, serverFileName, requestedTaskId);
        }
    }
}
