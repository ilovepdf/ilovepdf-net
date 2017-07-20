using JWT;
using LovePdf.Model.Enums;
using LovePdf.Model.Exception;
using LovePdf.Model.TaskParams;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace LovePdf.Core
{
    internal class RequestHelper
    {
        private string _privateKey;
        private string _publicKey;
        private readonly short _jwtDelay = 5400;

        private string Gwt { get; set; }
        private string EncryptKey { get; set; }
        private static RequestHelper _instance;
        private RequestHelper() { }

        public static RequestHelper Instance => _instance ?? (_instance = new RequestHelper());

        private static Exception ParseRequestErrors(HttpResponseMessage response, string responseContent, Exception exception)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) // 400 Bad Request
            {
                dynamic parsedContent = JObject.Parse(responseContent);

                if (parsedContent.error.type == EnumExtensions.GetEnumDescription(LovePdfErrors.ProcessingError))
                    return new ProcessingException(responseContent, exception);

                if (parsedContent.error.type == EnumExtensions.GetEnumDescription(LovePdfErrors.DownloadError))
                    return new DownloadException(responseContent, exception);

                if (parsedContent.error.type == EnumExtensions.GetEnumDescription(LovePdfErrors.UploadError))
                    return new UploadException(responseContent, exception);

                return exception;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized) // 401 Unauthorized
                return new AuthenticationException(responseContent, exception);

            if (response.StatusCode == HttpStatusCode.NotFound) // 404 Not Found
                return new NotFoundException(responseContent, exception);

            if (response.StatusCode == (HttpStatusCode)429) // 429 Too many Requests
                return new TooManyRequestsException(responseContent, exception);

            if ((int)response.StatusCode >= 500 && (int)response.StatusCode < 600) // 5xx Server Errors
                return new ServerErrorException(responseContent, exception);

            return exception;
        }

        public StartTaskResponse StartTask(string tool)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddAuthorizationHeader(httpClient);

                    var link = StringHelpers.Invariant($"{Settings.StartUrl}/{Settings.V1}/start/{tool}");

                    var requestMessage = new HttpRequestMessage(HttpMethod.Get, link);
                    response = httpClient.SendAsync(requestMessage).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<StartTaskResponse>(responseContent);

                }

            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
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
        public ExecuteTaskResponse ExecuteTask(Uri serverUrl, string taskId, List<FileModel> files, string tool, BaseParams parameters)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddAuthorizationHeader(httpClient);

                    var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/process");

                    var initalValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("task", taskId),
                        new KeyValuePair<string, string>("tool", tool),
                        new KeyValuePair<string, string>("v", $"net.{Settings.NetVersion}"),
                        //new KeyValuePair<string, string>("debug", "true"),
                    };

                    var multipartFormDataContent = new MultipartFormDataContent();

                    SetFormDataForExecuteTask(parameters, files, initalValues, multipartFormDataContent);

                    response = httpClient.PostAsync(link, multipartFormDataContent).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<ExecuteTaskResponse>(responseContent);

                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }

        }

        public void Download(Uri serverUrl, string taskId, string destinationPath)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    AddAuthorizationHeader(client);
                    var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/download/{taskId}");

                    var request = new HttpRequestMessage(HttpMethod.Get, link);
                    response = client.SendAsync(request).Result;

                    if (!response.IsSuccessStatusCode)
                        responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    using (var inputStream = response.Content.ReadAsStreamAsync().Result)
                    {
                        var fileName = response.Content.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                        using (var outputStream = new FileStream(destinationPath + "\\" + fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            inputStream.CopyTo(outputStream);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }

        }

        public async Task<byte[]> DownloadAsync(Uri serverUrl, string taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    AddAuthorizationHeader(client);
                    var link = $"{serverUrl}{Settings.V1}/download/{taskId}";

                    var request = new HttpRequestMessage(HttpMethod.Get, link);

                    response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                        responseContent = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

        public byte[] Download(string serverUrl, string taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    AddAuthorizationHeader(client);
                    var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/download/{taskId}");
                    var request = new HttpRequestMessage(HttpMethod.Get, link);

                    response = client.SendAsync(request).Result;

                    if (!response.IsSuccessStatusCode)
                        responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return response.Content.ReadAsByteArrayAsync().Result;

                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }
        public UploadTaskResponse UploadFile(Uri serverUrl, byte[] fileByteArray, string fileName, string taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;

            var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/upload");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddAuthorizationHeader(httpClient);
                    var multiPart = new MultipartFormDataContent();
                    var uploadRequest = new BaseTaskRequest();

                    uploadRequest.FormData.Add("task", taskId);
                    uploadRequest.FormData.Add("file", new FileParameter(fileByteArray, fileName));
                    SetMultiPartFormData(uploadRequest.FormData, multiPart);

                    response = httpClient.PostAsync(link, multiPart).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

        public UploadTaskResponse UploadFile(Uri serverUrl, Uri url, string taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;

            var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/upload");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddAuthorizationHeader(httpClient);
                    var multiPart = new MultipartFormDataContent();
                    var request = new BaseTaskRequest();
                    request.FormData.Add("cloud_file", url.AbsoluteUri);
                    request.FormData.Add("task", taskId);
                    SetMultiPartFormData(request.FormData, multiPart);

                    response = httpClient.PostAsync(link, multiPart).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);

                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

        public UploadTaskResponse UploadFile(Uri serverUrl, FileInfo file, string taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;

            var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/upload");

            try
            {
                using (Stream fs = file.OpenRead())
                {
                    var uploadRequest = new BaseTaskRequest();
                    uploadRequest.FormData.Add("file", new FileParameter(fs, file.Name));
                    uploadRequest.FormData.Add("task", taskId);
                    //uploadRequest.FormData.Add("debug", "true");

                    using (var httpClient = new HttpClient())
                    {
                        AddAuthorizationHeader(httpClient);
                        var multipartFormData = new MultipartFormDataContent();
                        SetMultiPartFormData(uploadRequest.FormData, multipartFormData);

                        response = httpClient.PostAsync(link, multipartFormData).Result;

                        responseContent = response.Content.ReadAsStringAsync().Result;

                        response.EnsureSuccessStatusCode();

                        return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                    }
                }

            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

        public UploadTaskResponse UploadFileByChunk(Uri serverUrl, FileInfo fileInfo, string taskId)
        {
            UploadTaskResponse results;
            HttpResponseMessage response = null;
            var responseContent = string.Empty;

            var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/upload");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    AddAuthorizationHeader(httpClient);

                    var uploadRequest = new BaseTaskRequest();
                    uploadRequest.FormData.Add("task", taskId);

                    //set default values
                    uploadRequest.FormData.Add("file", "");
                    uploadRequest.FormData.Add("chunk", "");

                    List<byte[]> chunksToUpload;

                    using (var fs = fileInfo.OpenRead())
                    {
                        chunksToUpload = ToChunks(fs);

                        uploadRequest.FormData.Add("chunks", chunksToUpload.Count.ToString(CultureInfo.InvariantCulture));
                    }

                    for (var i = 0; i < chunksToUpload.Count; i++)
                    {
                        using (var multipartFormDataContent = new MultipartFormDataContent())
                        {
                            uploadRequest.FormData["chunk"] = i.ToString(CultureInfo.InvariantCulture);
                            uploadRequest.FormData["file"] = new FileParameter(chunksToUpload[i], fileInfo.Name);

                            SetMultiPartFormData(uploadRequest.FormData, multipartFormDataContent);

                            response = httpClient.PostAsync(link, multipartFormDataContent).Result;

                            responseContent = response.Content.ReadAsStringAsync().Result;

                            response.EnsureSuccessStatusCode();
                        }
                    }

                    results = JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }

            return results;
        }

        public StatusTaskResponse CheckTaskStatus(Uri serverUrl, string taskId)
        {
            var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/task/{taskId}");

            HttpResponseMessage response = null;
            var responseContent = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    AddAuthorizationHeader(client);
                    response = client.GetAsync(link).Result;
                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<StatusTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

        public DeleteTaskResponse DeleteTask(Uri serverUrl, string taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;
            var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/task/{taskId}");

            try
            {
                using (var http = new HttpClient())
                {
                    AddAuthorizationHeader(http);

                    response = http.DeleteAsync(link).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<DeleteTaskResponse>(responseContent);

                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

        public void DeleteFile(Uri serverUrl, string serverFileName, string taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = string.Empty;

            try
            {
                var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/upload/delete");

                using (var http = new HttpClient())
                {
                    AddAuthorizationHeader(http);

                    var multipartFormDataContent = new MultipartFormDataContent();

                    var deleteRequest = new BaseTaskRequest();

                    deleteRequest.FormData.Add("task", taskId);
                    deleteRequest.FormData.Add("server_filename", serverFileName);

                    SetMultiPartFormData(deleteRequest.FormData, multipartFormDataContent);

                    response = http.PostAsync(link, multipartFormDataContent).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

        #region Help methods
        private static List<byte[]> ToChunks(Stream fileStream)
        {
            byte[] incomingArray;
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                incomingArray = ms.ToArray();
            }

            var result = new List<byte[]>();

            var incomingOffset = 0;

            while (incomingOffset < incomingArray.Length)
            {

                var length =
                   Math.Min(Settings.MaxBytesPerChunk, incomingArray.Length - incomingOffset);
                var outboundBuffer = new byte[length];

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
                encryptKey = Guid.NewGuid().ToString("n").Substring(0, 32);

            if (string.IsNullOrWhiteSpace(encryptKey)) return this;

            if (encryptKey.Length != 16 && encryptKey.Length != 24 && encryptKey.Length != 32)
                throw new ArgumentOutOfRangeException(nameof(encryptKey), "Only keys of sizes 16, 24 or 32 are supported.");

            EncryptKey = encryptKey;

            return this;
        }

        /// <summary>
        /// Set authorization header for http client
        /// </summary>
        /// <param name="httpClient"></param>
        private void AddAuthorizationHeader(HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(Gwt))
            {
                Gwt = GetJwt();
            }
            else if (IsExpiredGwt())
            {
                Gwt = GetJwt();
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Gwt);
        }

        /// <summary>
        /// Check if GWT token expired
        /// </summary>
        /// <returns></returns>
        private bool IsExpiredGwt()
        {
            return JwtHelper.CheckExpired(Gwt, _privateKey);
        }

        /// <summary>
        /// Generate GWT token
        /// </summary>
        /// <returns></returns>
        private string GetJwt()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var payLoad = new Dictionary<string, object>
            {
                {"iss", ""},
                {"aud", ""},
                {"iat", DateTime.UtcNow.AddSeconds(- _jwtDelay)},
                {"nbf", DateTime.UtcNow.AddSeconds(- _jwtDelay)},
                //Add 2 hours of expiration
                {
                    "exp",
                    Math.Round(new TimeSpan(DateTime.UtcNow.AddSeconds(_jwtDelay).Ticks).TotalSeconds -
                               new TimeSpan(epoch.Ticks).TotalSeconds)
                },
                {"jti", _publicKey }
            };
            if (!string.IsNullOrWhiteSpace(EncryptKey))
            {
                payLoad.Add("file_encryption_key", EncryptKey);
            }
            var token = JwtHelper.Encode(payLoad, _privateKey, JwtHashAlgorithm.HS256);

            return token;
        }

        private static void SetMultiPartFormData(Dictionary<string, object> formData, MultipartFormDataContent multiPartFormDataContent)
        {
            foreach (var param in formData)
            {

                if (param.Value is FileParameter file)
                {
                    var uploadFile = file;

                    if (uploadFile.FileStream != null)
                        multiPartFormDataContent.Add(new StreamContent(uploadFile.FileStream), "file", uploadFile.FileName);
                    else
                        multiPartFormDataContent.Add(new ByteArrayContent(uploadFile.File), "file", uploadFile.FileName);

                }
                else
                {
                    multiPartFormDataContent.Add(new StringContent((string)param.Value), param.Key);
                }
            }
        }

        private static void SetFormDataForExecuteTask(BaseParams @params, IReadOnlyList<FileModel> files, List<KeyValuePair<string, string>> initialValues, MultipartFormDataContent postMultipartFormDataContent)
        {
            if (@params != null)
            {
                //Serializing and deserializing to get properties from derived class, since those properties only available in runtime.
                var json = JsonConvert.SerializeObject(@params, new KeyValuePairConverter());
                var paramArray = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                initialValues.AddRange(
                    paramArray.Keys.Select(
                        paramKey => new KeyValuePair<string, string>(paramKey, paramArray[paramKey])));
            }

            for (var i = 0; i < files.Count; i++)
            {
                initialValues.Add(new KeyValuePair<string, string>(StringHelpers.Invariant($"files[{i}][filename]"), files[i].FileName));
                initialValues.Add(new KeyValuePair<string, string>(StringHelpers.Invariant($"files[{i}][server_filename]"), files[i].ServerFileName));
                initialValues.Add(new KeyValuePair<string, string>(StringHelpers.Invariant($"files[{i}][rotate]"), ((int)files[i].Rotate).ToString(CultureInfo.InvariantCulture)));
                initialValues.Add(new KeyValuePair<string, string>(StringHelpers.Invariant($"files[{i}][password]"), files[i].Password));
            }

            var filteredFormDataValues = initialValues.Where(x => !string.IsNullOrWhiteSpace(x.Value));
            foreach (var formDataValues in filteredFormDataValues)
            {
                postMultipartFormDataContent.Add(new StringContent(formDataValues.Value),
                    StringHelpers.Invariant($"\"{formDataValues.Key}\""));
            }
        }
        #endregion
    }
}
