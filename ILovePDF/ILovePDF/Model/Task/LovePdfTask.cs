﻿using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LovePdf.Core;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// ILovePdf Task
    /// </summary>
    public abstract class LovePdfTask
    {
        /// <summary>
        /// Server URL
        /// </summary>
        public Uri ServerUrl { get; set; }
        /// <summary>
        /// Task ID
        /// </summary>
        public string TaskId { get; set; }

        private List<FileModel> Files { get; set; }

        /// <summary>
        /// Get current running tool name
        /// </summary>
        /// <returns></returns>
        public virtual string ToolName => string.Empty;

        /// <summary>
        /// Set server url and task id
        /// </summary>
        /// <param name="serverUrl">server url from Create task response</param>
        /// <param name="taskId">Task id from Create task response</param>
        public void SetServerTaskId(Uri serverUrl, string taskId)
        {
            ServerUrl = serverUrl;
            TaskId = taskId;

            Files = new List<FileModel>();
        }

        /// <summary>
        /// Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Server file name</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse AddFile(string path)
        {
            return AddFile(path, TaskId);
        }

        /// <summary>
        /// Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <returns>Server file name</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse AddFile(string path, string taskId)
        {
            return AddFile(path, taskId, string.Empty);
        }

        /// <summary>
        /// Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="password"></param>
        /// <returns>Server file name</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse AddFile(string path, string taskId, string password)
        {
            return AddFile(path, taskId, password, Rotate.Degrees0);
        }

        /// <summary>
        /// Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse AddFile(string path, string taskId, Rotate rotate)
        {
            return AddFile(path, taskId, string.Empty, rotate);
        }

        internal void AddFiles(Dictionary<string, string> files)
        {
            foreach (var kvp in files)
            {
                Files.Add(new FileModel { ServerFileName = kvp.Key, FileName = kvp.Value });
            }
        }

        /// <summary>
        /// Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="password"></param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse AddFile(string path, string taskId, string password, Rotate rotate)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File not found", fileInfo.FullName);
            }

            var response = RequestHelper.Instance.UploadFile(ServerUrl, fileInfo, taskId);

            Files.Add(new FileModel() { ServerFileName = response.ServerFileName, FileName = fileInfo.Name, Password = password, Rotate = rotate });

            return response;
        }

        /// <summary>
        /// Upload file to the ILovePdf server by chunks
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFileByChunks(string path)
        {
            return AddFileByChunks(path, TaskId);
        }

        /// <summary>
        /// Upload file to the ILovePdf server by chunks
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFileByChunks(string path, string taskId)
        {
            return AddFileByChunks(path, taskId, string.Empty);
        }

        /// <summary>
        /// Upload file to the ILovePdf server by chunks
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="password"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFileByChunks(string path, string taskId, string password)
        {
            return AddFileByChunks(path, taskId, password, Rotate.Degrees0);
        }

        /// <summary>
        /// Upload file to the ILovePdf server by chunks
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFileByChunks(string path, string taskId, Rotate rotate)
        {
            return AddFileByChunks(path, taskId, string.Empty, rotate);
        }

        /// <summary>
        /// Upload file to the ILovePdf server by chunks
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="password"></param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFileByChunks(string path, string taskId, string password, Rotate rotate)
        {
            var fileInfo = new FileInfo(path);

            //Throw exception if file not found
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("File not found", fileInfo.FullName);
            }

            var response = RequestHelper.Instance.UploadFileByChunk(ServerUrl, fileInfo, taskId);

            Files.Add(new FileModel { ServerFileName = response.ServerFileName, FileName = fileInfo.Name, Password = password, Rotate = rotate });

            return response;
        }

        /// <summary>
        /// Upload file to the ILovePdf server as byte array.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFile(byte[] file)
        {
            return AddFile(file, TaskId);
        }

        /// <summary>
        /// Upload file to the ILovePdf server as byte array.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFile(byte[] file, string taskId)
        {
            return AddFile(file, taskId, taskId);
        }

        /// <summary>
        /// Upload file to the ILovePdf server as byte array.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="fileName">specify file name with extension otherwise file name will be the same as task id.</param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFile(byte[] file, string taskId, string fileName)
        {
            return AddFile(file, taskId, fileName, string.Empty);
        }

        /// <summary>
        /// Upload file to the ILovePdf server as byte array.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="fileName">specify file name with extension otherwise file name will be the same as task id.</param>
        /// <param name="password"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFile(byte[] file, string taskId, string fileName, string password)
        {
            return AddFile(file, taskId, fileName, password, Rotate.Degrees0);
        }

        /// <summary>
        /// Upload file to the ILovePdf server as byte array.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="fileName">specify file name with extension otherwise file name will be the same as task id.</param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFile(byte[] file, string taskId, string fileName, Rotate rotate)
        {
            return AddFile(file, taskId, fileName, string.Empty, rotate);
        }

        /// <summary>
        /// Upload file to the ILovePdf server as byte array.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="fileName">specify file name with extension otherwise file name will be the same as task id.</param>
        /// <param name="password"></param>
        /// <param name="rotate"></param>
        /// <returns>Server file name</returns>
        public UploadTaskResponse AddFile(byte[] file, string taskId, string fileName, string password, Rotate rotate)
        {
            var response = RequestHelper.Instance.UploadFile(ServerUrl, file, fileName, taskId);

            Files.Add(new FileModel { ServerFileName = response.ServerFileName, FileName = fileName, Password = password, Rotate = rotate });

            return response;
        }

        /// <summary>
        /// Upload file to the ILovePdf server from the url.
        /// </summary>
        /// <param name="cloudLink"></param>
        /// <returns>Server file name.</returns>
        public UploadTaskResponse AddFile(Uri cloudLink)
        {
            return AddFile(cloudLink, TaskId);
        }

        /// <summary>
        /// Upload file to the ILovePdf server from the url.
        /// </summary>
        /// <param name="cloudLink"></param>
        /// <param name="taskId"></param>
        /// <returns>Server file name.</returns>
        public UploadTaskResponse AddFile(Uri cloudLink, string taskId)
        {
            return AddFile(cloudLink, taskId, string.Empty);
        }

        /// <summary>
        /// Upload file to the ILovePdf server from the url.
        /// </summary>
        /// <param name="cloudLink"></param>
        /// <param name="taskId"></param>
        /// <param name="password">protect file with password</param>
        /// <returns>Server file name.</returns>
        public UploadTaskResponse AddFile(Uri cloudLink, string taskId, string password)
        {
            return AddFile(cloudLink, taskId, password, Rotate.Degrees0);
        }

        /// <summary>
        /// Upload file to the ILovePdf server from the url.
        /// </summary>
        /// <param name="cloudLink"></param>
        /// <param name="taskId"></param>
        /// <param name="rotate">protect file with password</param>
        /// <returns>Server file name.</returns>
        public UploadTaskResponse AddFile(Uri cloudLink, string taskId, Rotate rotate)
        {
            return AddFile(cloudLink, taskId, string.Empty, rotate);
        }

        /// <summary>
        /// Upload file to the ILovePdf server from the url.
        /// </summary>
        /// <param name="cloudLink"></param>
        /// <param name="taskId"></param>
        /// <param name="password">protect file with password</param>
        /// <param name="rotate">rotate file on: 0,90,180,20</param>
        /// <returns>Server file name.</returns>
        public UploadTaskResponse AddFile(Uri cloudLink, string taskId, string password, Rotate rotate)
        {
            if (cloudLink == null)
                throw new ArgumentException("cannot be null", nameof(cloudLink));

            var requestTaskId = string.IsNullOrWhiteSpace(taskId) ? TaskId : taskId;

            var response = RequestHelper.Instance.UploadFile(ServerUrl, cloudLink, requestTaskId);

            //TODO check filename
            var fileName = Path.GetFileName(cloudLink.AbsoluteUri);
            Files.Add(new FileModel { ServerFileName = response.ServerFileName, FileName = fileName, Password = password, Rotate = rotate });

            return response;
        }

        /// <summary>
        /// Execute current task
        /// </summary>
        public ExecuteTaskResponse Process(BaseParams parameters)
        {
            var executeResult = RequestHelper.Instance
                 .ExecuteTask(ServerUrl, TaskId, Files, ToolName, parameters);

            //Remove files after each task execute. To prevent processing same files.
            Files.Clear();

            return executeResult;
        }

        /// <summary>
        /// Download output files(s) from ILovePDF server to the specific location.
        /// </summary>
        public void DownloadFile()
        {
            DownloadFile(Environment.CurrentDirectory);
        }

        /// <summary>
        /// Download output files(s) from ILovePDF server to the specific location.
        /// </summary>
        /// <param name="destination">If no path is set, it will be donwloaded on current folder</param>
        public void DownloadFile(string destination)
        {
            DownloadFile(destination, TaskId);
        }

        /// <summary>
        /// Download output files(s) from ILovePDF server to the specific location.
        /// </summary>
        /// <param name="destination">If no path is set, it will be donwloaded on current folder</param>
        /// <param name="taskId">task id</param>
        public void DownloadFile(string destination, string taskId)
        {
            RequestHelper.Instance.Download(ServerUrl, taskId, destination);
        }

        /// <summary>
        /// Download output file(s) from ILovePDF server as byte array
        /// </summary>
        /// <returns>by</returns>
        public Task<byte[]> DownloadFileAsByteArrayAsync()
        {
            return DownloadFileAsByteArrayAsync(TaskId);
        }

        /// <summary>
        /// Download output file(s) from ILovePDF server as byte array
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>by</returns>
        public Task<byte[]> DownloadFileAsByteArrayAsync(string taskId)
        {
            return RequestHelper.Instance.DownloadAsync(ServerUrl, taskId);
        }

        /// <summary>
        /// Check task status. If no task passed will be checked status of the last one.
        /// </summary>
        /// <returns></returns>
        public StatusTaskResponse CheckTaskStatus()
        {
            return CheckTaskStatus(TaskId);
        }

        /// <summary>
        /// Check task status. If no task passed will be checked status of the last one.
        /// </summary>
        /// <param name="taskId">task to check.</param>
        /// <returns></returns>
        public StatusTaskResponse CheckTaskStatus(string taskId)
        {
            return RequestHelper.Instance.CheckTaskStatus(ServerUrl, taskId);
        }

        /// <summary>
        /// Delete specific task. If no task passed wil be deleted the last one.
        /// </summary>
        /// <returns></returns>
        public DeleteTaskResponse DeleteTask()
        {
            return DeleteTask(TaskId);
        }

        /// <summary>
        /// Delete specific task. If no task passed wil be deleted the last one.
        /// </summary>
        /// <param name="taskId">task to delete.</param>
        /// <returns></returns>
        public DeleteTaskResponse DeleteTask(string taskId)
        {
            var requestedTaskId = string.IsNullOrWhiteSpace(taskId) ? TaskId : taskId;

            return RequestHelper.Instance.DeleteTask(ServerUrl, requestedTaskId);
        }

        /// <summary>
        /// If you need to apply different tools on the same files, connected task resource will allows
        /// you to execute a new task on the files resulting from the previous tool. Using this resource
        /// you don't need to upload your files again. The response will contain the new task id and the
        /// files (the server_filename as key and filename as value), with server filename and the file
        /// name, you will need for the process step. Once the new connected task is created you can add,
        /// remove files, and work like another tool.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Next<T>()
            where T : LovePdfTask
        {
            return LovePdfApi.ConnectTask<T>(this);
        }

        /// <summary>
        /// Delete file from server.
        /// </summary>
        /// <param name="serverFileName">file name on server</param>
        public void DeleteFile(string serverFileName)
        {
            DeleteFile(serverFileName, TaskId);
        }

        /// <summary>
        /// Delete file from server.
        /// </summary>
        /// <param name="serverFileName">file name on server</param>
        /// <param name="task"></param>
        public void DeleteFile(string serverFileName, string task)
        {
            if (string.IsNullOrWhiteSpace(serverFileName))
            {
                throw new ArgumentNullException(nameof(serverFileName), "Specify server file name");
            }
            var requestedTaskId = string.IsNullOrWhiteSpace(task) ? TaskId : task;

            RequestHelper.Instance.DeleteFile(ServerUrl, serverFileName, requestedTaskId);
        }
    }
}
