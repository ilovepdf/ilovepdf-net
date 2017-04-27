using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using ILovePDF.Core;
using ILovePDF.Core.Model;
using ILovePDF.Core.TaskRequest;
using JWT;
using Newtonsoft.Json;
using ILovePDF.DTO;
using ILovePDF.Model.Task;
using ILovePDF.Model.TaskParams;
using Newtonsoft.Json.Converters;
using ILovePDF.Core.TaskResponse;

namespace ILovePDF
{
    internal class RequestHelper
    {
        private string _privateKey;
        private string _publicKey;

        string GWT { get; set; }
        private string EncryptKey { get; set; }
        private static RequestHelper _instance;
        private RequestHelper() { }

        public static RequestHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RequestHelper();
                }

                return _instance;
            }
        }

        public StartTaskResponse StartTask(string tool)
        {
            string responseContent = string.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    this.AddAuthorizationHeader(httpClient);

                    var link = string.Format("{0}/{1}/start/{2}", Settings.StartUrl, Settings.v1, tool);

                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, link);
                    var responseMessage = httpClient.SendAsync(requestMessage).Result;

                    responseContent = responseMessage.Content.ReadAsStringAsync().Result;

                    //throw exception if status code is not 200
                    responseMessage.EnsureSuccessStatusCode();

                    var result = JsonConvert.DeserializeObject<StartTaskResponse>(responseContent);
                    return result;
                }
            }
            catch (Exception e)
            {

                throw new HttpRequestException(responseContent, e);
            }

        }

        /// <summary>
        /// Execute current task
        /// </summary>
        /// <param name="tool">tool name for current task</param>
        /// <param name="parameters">specific parameters for current task</param>
        /// <param name="serverUrl">server url</param>
        /// <param name="taskId"> current task id</param>
        /// <param name="files">file</param>
        /// <returns>time to process the task.</returns>
        public string ExecuteTask(string serverUrl, string taskId, List<FileModel> files, string tool, BaseParams parameters)
        {
            string responseContent = string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    this.AddAuthorizationHeader(httpClient);

                    var link = string.Format("{0}{1}/{2}/process", Settings.Host, serverUrl, Settings.v1);

                    var initalValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("task", taskId),
                        new KeyValuePair<string, string>("tool", tool),
                    };

                    var multipartFormDataContent = new MultipartFormDataContent();

                    this.SetFormDataForExecuteTask(parameters, files, initalValues, multipartFormDataContent);

                    var responseMessage = httpClient.PostAsync(link, multipartFormDataContent).Result;

                    responseContent = responseMessage.Content.ReadAsStringAsync().Result;

                    responseMessage.EnsureSuccessStatusCode();

                    return responseContent;
                }
            }
            catch (Exception e)
            {

                throw new HttpRequestException(responseContent, e);
            }

        }

        public void Download(string serverUrl, string taskId, string destinationPath)
        {
            string responseContent = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    this.AddAuthorizationHeader(client);
                    var link = string.Format("{0}{1}/{2}/download/{3}", Settings.Host, serverUrl, Settings.v1, taskId);

                    var request = new HttpRequestMessage(HttpMethod.Get, link);
                    var responseMessage = client.SendAsync(request).Result;

                    if (!responseMessage.IsSuccessStatusCode)
                        responseContent = responseMessage.Content.ReadAsStringAsync().Result;

                    //Thorw exception if status code not 200
                    responseMessage.EnsureSuccessStatusCode();

                    using (var inputStream = responseMessage.Content.ReadAsStreamAsync().Result)
                    {
                        var fileName = responseMessage.Content.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                        using (var outputStream = new FileStream(destinationPath + "\\" + fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            inputStream.CopyTo(outputStream);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new HttpRequestException(responseContent, e);

            }

        }

        public async Task<byte[]> DownloadAsync(string serverUrl, string taskId)
        {
            string responseContent = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    this.AddAuthorizationHeader(client);
                    var link = string.Format("{0}{1}/{2}/download/{3}", Settings.Host, serverUrl, Settings.v1, taskId);

                    var request = new HttpRequestMessage(HttpMethod.Get, link);

                    var responseMessage = await client.SendAsync(request);

                    if (!responseMessage.IsSuccessStatusCode)
                        responseContent = await responseMessage.Content.ReadAsStringAsync();

                    //Thorw exception if status code not 200
                    responseMessage.EnsureSuccessStatusCode();

                   return await responseMessage.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception e)
            {

                throw new HttpRequestException(responseContent, e);
            }
        }

        public byte[] Download(string serverUrl, string taskId)
        {
            string responseContent = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    this.AddAuthorizationHeader(client);
                    var link = string.Format("{0}{1}/{2}/download/{3}", Settings.Host, serverUrl, Settings.v1, taskId);
                    var request = new HttpRequestMessage(HttpMethod.Get, link);

                    var responseMessage = client.SendAsync(request).Result;

                    if (!responseMessage.IsSuccessStatusCode)
                        responseContent = responseMessage.Content.ReadAsStringAsync().Result;

                    //Thorw exception if status code not 200
                    responseMessage.EnsureSuccessStatusCode();

                    return responseMessage.Content.ReadAsByteArrayAsync().Result;

                }
            }
            catch (Exception e)
            {
                throw new HttpRequestException(responseContent, e);
            }
        }
        public UploadTaskResponse UploadFile(string serverUrl, byte[] fileByteArray, string fileName, string taskId)
        {
            string responseContent = string.Empty;

            var link = string.Format("{0}{1}/{2}/upload", Settings.Host, serverUrl, Settings.v1);

            try
            {
                using (var httpClient = new HttpClient())
                {
                    this.AddAuthorizationHeader(httpClient);
                    var multiPart = new MultipartFormDataContent();
                    var uploadRequest = new BaseTaskRequest();

                    uploadRequest.FormData.Add("task", taskId);
                    uploadRequest.FormData.Add("file", new FileParameter(fileByteArray, fileName));
                    this.SetMultiPartFormData(uploadRequest.FormData, multiPart);

                    var response = httpClient.PostAsync(link, multiPart).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {

                throw new HttpRequestException(responseContent, e);
            }
        }
        public UploadTaskResponse UploadFile(string serverUrl, Uri url, string taskId)
        {
            string responseContent = string.Empty;

            var link = string.Format("{0}{1}/{2}/upload", Settings.Host, serverUrl, Settings.v1);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    this.AddAuthorizationHeader(httpClient);
                    var multiPart = new MultipartFormDataContent();
                    var request = new BaseTaskRequest();
                    request.FormData.Add("cloud_file", url.AbsoluteUri);
                    request.FormData.Add("task", taskId);
                    this.SetMultiPartFormData(request.FormData, multiPart);

                    var response = httpClient.PostAsync(link, multiPart).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);

                }
            }
            catch (Exception e)
            {

                throw new HttpRequestException(responseContent, e);
            }
        }

        public UploadTaskResponse UploadFile(string serverUrl, FileInfo file, string taskId)
        {
            string responseContent = string.Empty;

            var link = string.Format("{0}{1}/{2}/upload", Settings.Host, serverUrl, Settings.v1);

            try
            {
                using (Stream fs = file.OpenRead())
                {
                    var uploadRequest = new BaseTaskRequest();
                    uploadRequest.FormData.Add("file", new FileParameter(fs, file.Name));
                    uploadRequest.FormData.Add("task", taskId);

                    using (var httpClient = new HttpClient())
                    {
                        this.AddAuthorizationHeader(httpClient);
                        var multipartFormData = new MultipartFormDataContent();
                        this.SetMultiPartFormData(uploadRequest.FormData, multipartFormData);

                        var response = httpClient.PostAsync(link, multipartFormData).Result;

                        responseContent = response.Content.ReadAsStringAsync().Result;

                        //Throw exception is status code is not 200
                        response.EnsureSuccessStatusCode();

                        return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);

                    }
                }

            }
            catch (Exception e)
            {
                throw new HttpRequestException(responseContent, e);
            }
        }

        public UploadTaskResponse UploadFileByChunk(string serverUrl, FileInfo fileInfo, string taskId)
        {
            UploadTaskResponse results = null;
            string responseContent = string.Empty;

            var link = string.Format("{0}{1}/{2}/upload", Settings.Host, serverUrl, Settings.v1);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    this.AddAuthorizationHeader(httpClient);

                    var uploadRequest = new BaseTaskRequest();
                    uploadRequest.FormData.Add("task", taskId);

                    //set default values
                    uploadRequest.FormData.Add("file", "");
                    uploadRequest.FormData.Add("chunk", "");



                    List<byte[]> chunksToUpload;

                    using (var fs = fileInfo.OpenRead())
                    {
                        chunksToUpload = ToChunks(fs);

                        uploadRequest.FormData.Add("chunks", chunksToUpload.Count.ToString());
                    }

                    for (int i = 0; i < chunksToUpload.Count; i++)
                    {
                        using (var multipartFormDataContent = new MultipartFormDataContent())
                        {
                            uploadRequest.FormData["chunk"] = (i + 1).ToString();
                            uploadRequest.FormData["file"] = new FileParameter(chunksToUpload[i], fileInfo.Name);

                            this.SetMultiPartFormData(uploadRequest.FormData, multipartFormDataContent);

                            var response = httpClient.PostAsync(link, multipartFormDataContent).Result;

                            responseContent = response.Content.ReadAsStringAsync().Result;
                            //throw exception if stauscode is not 200
                            response.EnsureSuccessStatusCode();
                        }
                    }

                    results = JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {

                throw new HttpRequestException(responseContent, e);
            }

            return results;
        }

        public StatusTaskResponse CheckTaskStatus(string serverUrl, string taskId)
        {
            string link = string.Format("{0}{1}/{2}/task/{3}", Settings.Host, serverUrl, Settings.v1, taskId);

            string responeContent = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    this.AddAuthorizationHeader(client);
                    var response = client.GetAsync(link).Result;
                    var content = response.Content.ReadAsStringAsync().Result;

                    //throw exception if status code is not 200
                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<StatusTaskResponse>(content);
                }
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(responeContent, ex);
            }
        }

        public DeleteTaskResponse DeleteTask(string serverUrl, string taskId)
        {
            string responseContent = string.Empty;
            var link = string.Format("{0}{1}/{2}/task/{3}", Settings.Host, serverUrl, Settings.v1, taskId);

            try
            {
                using (var http = new HttpClient())
                {
                    this.AddAuthorizationHeader(http);

                    var response = http.DeleteAsync(link).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    //throw exception if status code not 200
                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<DeleteTaskResponse>(responseContent);

                }
            }
            catch (Exception e)
            {
                throw new HttpRequestException(responseContent, e);
            }
        }

        public void DeleteFile(string serverUrl, string serverFileName, string taskId)
        {
            string responseContent = string.Empty;

            try
            {
                var link = string.Format("{0}{1}/{2}/upload/delete", Settings.Host, serverUrl, Settings.v1);

                using (var http = new HttpClient())
                {
                    this.AddAuthorizationHeader(http);

                    var multipartFormDataContent = new MultipartFormDataContent();

                    var deleteRequest = new BaseTaskRequest();

                    deleteRequest.FormData.Add("task", taskId);
                    deleteRequest.FormData.Add("server_filename", serverFileName);

                    this.SetMultiPartFormData(deleteRequest.FormData, multipartFormDataContent);

                    var response = http.PostAsync(link, multipartFormDataContent).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    //Thorw exception if status code not 200
                    response.EnsureSuccessStatusCode();


                }
            }
            catch (Exception e)
            {
                throw new HttpRequestException(responseContent, e);
            }
        }

        #region Help methods
        private List<byte[]> ToChunks(Stream fileStream)
        {
            byte[] incomingArray;
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                incomingArray = ms.ToArray();
            }

            List<byte[]> result = new List<byte[]>();

            int incomingOffset = 0;

            while (incomingOffset < incomingArray.Length)
            {

                int length =
                   Math.Min(Settings.MaxBytesPerChunk, incomingArray.Length - incomingOffset);
                byte[] outboundBuffer = new byte[length];

                Buffer.BlockCopy(incomingArray, incomingOffset,
                                 outboundBuffer, 0,
                                 length);

                incomingOffset += length;

                // Transmit outbound buffer
                result.Add((byte[])outboundBuffer.Clone());
            }
            return result;
        }

        public RequestHelper SetKeys(string privateKey, string publicKey)
        {
            _privateKey = privateKey;
            _publicKey = publicKey;
            return this;
        }

        /// <summary>
        /// Set file encrypt key
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="useBuildIn"></param>
        /// <returns></returns>
        public RequestHelper SetEncryptKey(string encryptKey = "", bool useBuildIn = false)
        {
            //if using build in skip encryptKey param even if it is provided
            if (useBuildIn)
            {
                var key = Guid.NewGuid().ToString("n").Substring(0, new Random().Next(16, 32));

                EncryptKey = key;
            }
            else if (!string.IsNullOrWhiteSpace(encryptKey))
            {
                if (encryptKey.Length < 16 || encryptKey.Length > 32)
                {
                    throw new ArgumentOutOfRangeException(nameof(encryptKey), "Only keys of sizes 16, 24 or 32 are supported.");
                }

                EncryptKey = encryptKey;
            }
            return this;
        }

        /// <summary>
        /// Set authorization header for http client
        /// </summary>
        /// <param name="httpClient"></param>
        private void AddAuthorizationHeader(HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(GWT))
            {
                GWT = GetJWT();
            }
            else if (IsExpiredGWT())
            {
                GWT = GetJWT();
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GWT);
        }

        /// <summary>
        /// Check if GWT token expired
        /// </summary>
        /// <returns></returns>
        private bool IsExpiredGWT()
        {
            return JWTHelper.CheckExpired(GWT, _privateKey);
        }

        /// <summary>
        /// Generate GWT token
        /// </summary>
        /// <returns></returns>
        private string GetJWT()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var payLoad = new Dictionary<string, object>
            {
                {"iss", ""},
                {"aud", ""},
                {"iat", DateTime.UtcNow.AddSeconds(-600)},
                {"nbf", DateTime.UtcNow.AddSeconds(-600)},
                //Add 2 hours of expiration
                {
                    "exp",
                    Math.Round(new TimeSpan(DateTime.UtcNow.AddSeconds(3600).AddSeconds(600).Ticks).TotalSeconds -
                               new TimeSpan(epoch.Ticks).TotalSeconds)
                },
                {"jti", _publicKey }
            };
            if (!string.IsNullOrWhiteSpace(EncryptKey))
            {
                payLoad.Add("file_encryption_key", EncryptKey);
            }
            var token = JWTHelper.Encode(payLoad, _privateKey, JwtHashAlgorithm.HS256);

            return token;
        }

        private void SetMultiPartFormData(Dictionary<string, object> formData, MultipartFormDataContent multiPartFormDataContent)
        {
            foreach (var param in formData)
            {
                if (param.Value is FileParameter)
                {
                    var uploadFile = (FileParameter)param.Value;

                    if (uploadFile.FileStream != null)
                    {
                        multiPartFormDataContent.Add(new StreamContent(uploadFile.FileStream), "file", uploadFile.FileName);
                    }
                    else
                    {
                        multiPartFormDataContent.Add(new ByteArrayContent(uploadFile.File), "file", uploadFile.FileName);
                    }
                }
                else
                {
                    multiPartFormDataContent.Add(new StringContent((string)param.Value), param.Key);
                }
            }
        }

        private void SetFormDataForExecuteTask(BaseParams @params, List<FileModel> files, List<KeyValuePair<string, string>> initialValues, MultipartFormDataContent postMultipartFormDataContent)
        {
            if (@params != null)
            {
                //Serializing and deserializing to get properties from derived class, since those properties only available in runtime.
                string json = JsonConvert.SerializeObject(@params, new KeyValuePairConverter());
                Dictionary<string, string> paramArray = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                foreach (var paramKey in paramArray.Keys)
                {
                    initialValues.Add(new KeyValuePair<string, string>(paramKey, paramArray[paramKey]));
                }
            }

            for (int i = 0; i < files.Count; i++)
            {
                initialValues.Add(new KeyValuePair<string, string>(string.Format("files[{0}][filename]", i), files[i].FileName));
                initialValues.Add(new KeyValuePair<string, string>(string.Format("files[{0}][server_filename]", i), files[i].ServerFileName));
                initialValues.Add(new KeyValuePair<string, string>(string.Format("files[{0}][rotate]", i), ((int)files[i].Rotate).ToString()));
                initialValues.Add(new KeyValuePair<string, string>(string.Format("files[{0}][password]", i), files[i].Password));
            }

            var filteredFormDataValues = initialValues.Where(x => !string.IsNullOrWhiteSpace(x.Value));
            foreach (var formDataValues in filteredFormDataValues)
            {
                postMultipartFormDataContent.Add(new StringContent(formDataValues.Value),
                    string.Format("\"{0}\"", formDataValues.Key));
            }
        }
        #endregion
    }
}
