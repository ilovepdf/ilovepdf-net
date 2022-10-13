using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Jose;
using LovePdf.Extensions;
using LovePdf.Model.Enums;
using LovePdf.Model.Exception;
using LovePdf.Model.TaskParams;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace LovePdf.Core
{
    internal class RequestHelper
    {
        private static readonly DateTime epoch = 
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static readonly TimeSpan jwtTolerance = TimeSpan.FromMinutes(5);

        private static RequestHelper _instance;
        private readonly Int16 _jwtDelay = 5400;
        private Byte[] _privateKey;
        private String _publicKey;

        private RequestHelper()
        {
        }

        private String Gwt { get; set; }
        private String EncryptKey { get; set; }

        public static RequestHelper Instance => _instance ?? (_instance = new RequestHelper());

        private static Exception parseRequestErrors(HttpResponseMessage response, String responseContent,
            Exception exception)
        {
            if (response == null)
            {
                return exception;
            }

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

            if (response.StatusCode == (HttpStatusCode) 429) // 429 Too many Requests
                return new TooManyRequestsException(responseContent, exception);

            if ((Int32) response.StatusCode >= 500 && (Int32) response.StatusCode < 600) // 5xx Server Errors
                return new ServerErrorException(responseContent, exception);

            return exception;
        }

        public StartTaskResponse StartTask(String tool)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    addAuthorizationHeader(client);

                    var link = GetUri($"{Settings.StartUrl}/{Settings.V1}/start/{tool}");

                    using var requestMessage = new HttpRequestMessage(HttpMethod.Get, link);
                    response = httpClient.SendAsync(requestMessage).Result;
                    var request = new HttpRequestMessage(HttpMethod.Get, link);
                    response = client.Send(request);

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<StartTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public ConnectTaskResponse ConnectTask(String parentTaskId, String tool)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    addAuthorizationHeader(httpClient);

                    var link = GetUri($"{Settings.StartUrl}/{Settings.V1}/task/next");

                    using var multipartFormDataContent = new MultipartFormDataContent();

                    var request = new BaseTaskRequest();
                    request.FormData.Add("task", parentTaskId);
                    request.FormData.Add("tool", tool);

                    setMultiPartFormData(request.FormData, multipartFormDataContent);

                    response = httpClient.PostAsync(link, multipartFormDataContent).Result;
                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<ConnectTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        /// <summary>
        ///     Execute current task
        /// </summary>
        /// <param name="tool">tool name for current task</param>
        /// <param name="parameters">specific parameters for current task</param>
        /// <param name="serverUrl">server url</param>
        /// <param name="taskId"> current task id</param>
        /// <param name="files">file</param>
        /// <returns>time to process the task.</returns>
        public ExecuteTaskResponse ExecuteTask(Uri serverUrl, String taskId, List<FileModel> files, String tool,
            BaseParams parameters)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    addAuthorizationHeader(httpClient);

                    var link = GetUri($"{serverUrl}{Settings.V1}/process");

                    var initalValues = new List<KeyValuePair<String, String>>
                    {
                        new KeyValuePair<String, String>("task", taskId),
                        new KeyValuePair<String, String>("tool", tool),
                        new KeyValuePair<String, String>("v", $"net.{Settings.NetVersion}")
                        //new KeyValuePair<string, string>("debug", "true"),
                    };

                    using var multipartFormDataContent = new MultipartFormDataContent();

                    setFormDataForExecuteTask(parameters, files, initalValues, multipartFormDataContent);

                    response = httpClient.PostAsync(link, multipartFormDataContent).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<ExecuteTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public void Download(Uri serverUrl, String taskId, String destinationPath)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    addAuthorizationHeader(client);
                    var link = GetUri($"{serverUrl}{Settings.V1}/download/{taskId}");

                    var request = new HttpRequestMessage(HttpMethod.Get, link);
                    response = client.Send(request);
                    using var request = new HttpRequestMessage(HttpMethod.Get, link);
                    response = client.SendAsync(request).Result;

                    if (!response.IsSuccessStatusCode)
                        responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    using (var inputStream = response.Content.ReadAsStreamAsync().Result)
                    {
                        var fileName = response.Content.Headers.ContentDisposition.FileName
                            .Replace("\"", String.Empty);

                        using (var outputStream = new FileStream(
                            Path.Combine(destinationPath, fileName),
                            FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            inputStream.CopyTo(outputStream);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public async Task<Byte[]> DownloadAsync(Uri serverUrl, String taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    addAuthorizationHeader(client);
                    var link = $"{serverUrl}{Settings.V1}/download/{taskId}";

                    using var request = new HttpRequestMessage(HttpMethod.Get, link);

                    response = await client.SendAsync(request).ConfigureAwait(false); ;
                    var request = new HttpRequestMessage(HttpMethod.Get, link);
                    response = await client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                        responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false); ;

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false); ;
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public Byte[] Download(String serverUrl, String taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    addAuthorizationHeader(client);
                    var link = GetUri($"{serverUrl}{Settings.V1}/download/{taskId}");
                    using var request = new HttpRequestMessage(HttpMethod.Get, link);

                    response = client.SendAsync(request).Result;
                    var link = StringHelpers.Invariant($"{serverUrl}{Settings.V1}/download/{taskId}");
                    var request = new HttpRequestMessage(HttpMethod.Get, link);                   
                    response = client.Send(request);

                    if (!response.IsSuccessStatusCode)
                        responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return response.Content.ReadAsByteArrayAsync().Result;
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public UploadTaskResponse UploadFile(Uri serverUrl, Byte[] fileByteArray, String fileName, String taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            var link = GetUri($"{serverUrl}{Settings.V1}/upload");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    addAuthorizationHeader(httpClient);
                    using var multiPart = new MultipartFormDataContent();
                    var uploadRequest = new BaseTaskRequest();

                    uploadRequest.FormData.Add("task", taskId);
                    uploadRequest.FormData.Add("file", new FileParameter(fileByteArray, fileName));
                    setMultiPartFormData(uploadRequest.FormData, multiPart);

                    response = httpClient.PostAsync(link, multiPart).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public UploadTaskResponse UploadFile(Uri serverUrl, Uri url, String taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            var link = GetUri($"{serverUrl}{Settings.V1}/upload");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    addAuthorizationHeader(httpClient);
                    using var multiPart = new MultipartFormDataContent();
                    var request = new BaseTaskRequest();
                    request.FormData.Add("cloud_file", url.AbsoluteUri);
                    request.FormData.Add("task", taskId);
                    setMultiPartFormData(request.FormData, multiPart);

                    response = httpClient.PostAsync(link, multiPart).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public UploadTaskResponse UploadFile(Uri serverUrl, FileInfo file, String taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            var link = GetUri($"{serverUrl}{Settings.V1}/upload");

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
                        addAuthorizationHeader(httpClient);
                        using var multipartFormData = new MultipartFormDataContent();
                        setMultiPartFormData(uploadRequest.FormData, multipartFormData);

                        response = httpClient.PostAsync(link, multipartFormData).Result;

                        responseContent = response.Content.ReadAsStringAsync().Result;

                        response.EnsureSuccessStatusCode();

                        return JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                    }
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public UploadTaskResponse UploadFileByChunk(Uri serverUrl, FileInfo fileInfo, String taskId)
        {
            UploadTaskResponse results;
            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            var link = GetUri($"{serverUrl}{Settings.V1}/upload");
            try
            {
                using (var httpClient = new HttpClient())
                {
                    addAuthorizationHeader(httpClient);

                    var uploadRequest = new BaseTaskRequest();
                    uploadRequest.FormData.Add("task", taskId);

                    //set default values
                    uploadRequest.FormData.Add("file", "");
                    uploadRequest.FormData.Add("chunk", "");

                    List<Byte[]> chunksToUpload;

                    using (var fs = fileInfo.OpenRead())
                    {
                        chunksToUpload = toChunks(fs);

                        uploadRequest.FormData.Add("chunks",
                            chunksToUpload.Count.ToString(CultureInfo.InvariantCulture));
                    }

                    for (var i = 0; i < chunksToUpload.Count; i++)
                        using (var multipartFormDataContent = new MultipartFormDataContent())
                        {
                            uploadRequest.FormData["chunk"] = i.ToString(CultureInfo.InvariantCulture);
                            uploadRequest.FormData["file"] = new FileParameter(chunksToUpload[i], fileInfo.Name);

                            setMultiPartFormData(uploadRequest.FormData, multipartFormDataContent);

                            response = httpClient.PostAsync(link, multipartFormDataContent).Result;

                            responseContent = response.Content.ReadAsStringAsync().Result;

                            response.EnsureSuccessStatusCode();
                        }

                    results = JsonConvert.DeserializeObject<UploadTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }

            return results;
        }

        public StatusTaskResponse CheckTaskStatus(Uri serverUrl, String taskId)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/task/{taskId}");

            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    addAuthorizationHeader(client);
                    response = client.GetAsync(link).Result;
                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<StatusTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public DeleteTaskResponse DeleteTask(Uri serverUrl, String taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;
            var link = GetUri($"{serverUrl}{Settings.V1}/task/{taskId}");

            try
            {
                using (var http = new HttpClient())
                {
                    addAuthorizationHeader(http);

                    response = http.DeleteAsync(link).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();

                    return JsonConvert.DeserializeObject<DeleteTaskResponse>(responseContent);
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        public void DeleteFile(Uri serverUrl, String serverFileName, String taskId)
        {
            HttpResponseMessage response = null;
            var responseContent = String.Empty;

            try
            {
                var link = GetUri($"{serverUrl}{Settings.V1}/upload/delete");

                using (var http = new HttpClient())
                {
                    addAuthorizationHeader(http);

                    using var multipartFormDataContent = new MultipartFormDataContent();

                    var deleteRequest = new BaseTaskRequest();

                    deleteRequest.FormData.Add("task", taskId);
                    deleteRequest.FormData.Add("server_filename", serverFileName);

                    setMultiPartFormData(deleteRequest.FormData, multipartFormDataContent);

                    response = http.PostAsync(link, multipartFormDataContent).Result;

                    responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception e)
            {
                throw parseRequestErrors(response, responseContent, e);
            }
        }

        #region Help methods

        private static List<Byte[]> toChunks(Stream fileStream)
        {
            Byte[] incomingArray;
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                incomingArray = ms.ToArray();
            }

            var result = new List<Byte[]>();

            var incomingOffset = 0;

            while (incomingOffset < incomingArray.Length)
            {
                var length =
                    Math.Min(Settings.MaxBytesPerChunk, incomingArray.Length - incomingOffset);
                var outboundBuffer = new Byte[length];

                Buffer.BlockCopy(incomingArray, incomingOffset,
                    outboundBuffer, 0,
                    length);

                incomingOffset += length;

                // Transmit outbound buffer
                result.Add((Byte[]) outboundBuffer.Clone());
            }

            return result;
        }

        public RequestHelper SetKeys(String privateKey, String publicKey)
        {
            _privateKey = Encoding.UTF8.GetBytes(privateKey);
            _publicKey = publicKey;
            return this;
        }

        /// <summary>
        ///     Set file encrypt key
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="useBuildIn"></param>
        /// <returns></returns>
        public RequestHelper SetEncryptKey(String encryptKey = "", Boolean useBuildIn = false)
        {
            //if using build in skip encryptKey param even if it is provided
            if (useBuildIn)
                encryptKey = Guid.NewGuid().ToString("n").Substring(0, 32);

            if (String.IsNullOrWhiteSpace(encryptKey)) return this;

            if (encryptKey.Length != 16 && encryptKey.Length != 24 && encryptKey.Length != 32)
                throw new ArgumentOutOfRangeException(nameof(encryptKey),
                    "Only keys of sizes 16, 24 or 32 are supported.");

            EncryptKey = encryptKey;

            return this;
        }

        /// <summary>
        ///     Set authorization header for http client
        /// </summary>
        /// <param name="httpClient"></param>
        private void addAuthorizationHeader(HttpClient httpClient)
        {
            if (String.IsNullOrEmpty(Gwt) || isExpiredGwt())
            {
                Gwt = getJwt();
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Gwt);
        }

        /// <summary>
        ///     Check if GWT token expired
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031")]
        private Boolean isExpiredGwt()
        {
            try
            {
                JWT.Decode(Gwt, _privateKey, JwsAlgorithm.HS256);
                var expired = epoch.AddSeconds(
                    (JObject.Parse(JWT.Payload(Gwt))["exp"] ?? 0).Value<double>())
                    .Subtract(jwtTolerance);
                return expired < DateTime.UtcNow;
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        ///     Generate GWT token
        /// </summary>
        /// <returns></returns>
        private String getJwt()
        {
            var payLoad = new Dictionary<String, Object>
            {
                {"iss", ""},
                {"aud", ""},
                {"iat", DateTime.UtcNow.AddSeconds(-_jwtDelay)},
                {"nbf", DateTime.UtcNow.AddSeconds(-_jwtDelay)},
                //Add 2 hours of expiration
                {
                    "exp",
                    Math.Round(new TimeSpan(DateTime.UtcNow.AddSeconds(_jwtDelay).Ticks).TotalSeconds -
                               new TimeSpan(epoch.Ticks).TotalSeconds)
                },
                {"jti", _publicKey}
            };
            if (!String.IsNullOrWhiteSpace(EncryptKey)) payLoad.Add("file_encryption_key", EncryptKey);

            var token = JWT.Encode(payLoad, _privateKey, JwsAlgorithm.HS256);

            return token;
        }

        private static void setMultiPartFormData(Dictionary<String, Object> formData,
            MultipartFormDataContent multiPartFormDataContent)
        {
            foreach (var param in formData)
                if (param.Value is FileParameter file)
                {
                    var uploadFile = file;

                    if (uploadFile.FileStream != null)
                    {
                        using var content = new StreamContent(uploadFile.FileStream);
                        multiPartFormDataContent.Add(content, "file", uploadFile.FileName);
                    }
                    else 
                    {
                        using var content = new ByteArrayContent(uploadFile.File);
                        multiPartFormDataContent.Add(content, "file", uploadFile.FileName);
                    }
                      
                }
                else
                {
                    using var content = new StringContent((String)param.Value);
                    multiPartFormDataContent.Add(content, param.Key);
                }
        }

        private static void setFormDataForExecuteTask(BaseParams @params, IReadOnlyList<FileModel> files,
            List<KeyValuePair<String, String>> initialValues, MultipartFormDataContent postMultipartFormDataContent)
        {
            if (@params != null)
            {
                //Serializing and deserializing to get properties from derived class, since those properties only available in runtime.
                var json = JsonConvert.SerializeObject(@params, new KeyValuePairConverter());
                var paramArray = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);

                initialValues.AddRange(
                    paramArray.Keys.Select(
                        paramKey => new KeyValuePair<String, String>(paramKey, paramArray[paramKey])));
            }

            if (@params is WaterMarkParams watermarkParams)
            {
                var elements = watermarkParams.Elements;
                for (var index = 0; index < elements.Count; index++)
                {
                    var element = elements[index];

                    //Serializing and deserializing to get properties from derived class, since those properties only available in runtime.
                    var json = JsonConvert.SerializeObject(element, new KeyValuePairConverter());
                    var paramArray = JsonConvert.DeserializeObject<Dictionary<String, String>>(json);

                    initialValues.AddRange(paramArray.Keys.Select(
                        paramKey => new KeyValuePair<String, String>(
                            StringHelpers.Invariant($"elements[{index}][{paramKey}]"),
                            paramArray[paramKey])));
                }
            }

            for (var i = 0; i < files.Count; i++)
            {
                initialValues.Add(new KeyValuePair<String, String>(StringHelpers.Invariant($"files[{i}][filename]"),
                    files[i].FileName));
                initialValues.Add(new KeyValuePair<String, String>(
                    StringHelpers.Invariant($"files[{i}][server_filename]"), files[i].ServerFileName));
                initialValues.Add(new KeyValuePair<String, String>(StringHelpers.Invariant($"files[{i}][rotate]"),
                    ((Int32) files[i].Rotate).ToString(CultureInfo.InvariantCulture)));
                initialValues.Add(new KeyValuePair<String, String>(StringHelpers.Invariant($"files[{i}][password]"),
                    files[i].Password));
            }

            var filteredFormDataValues = initialValues.Where(x => !String.IsNullOrWhiteSpace(x.Value));
            foreach (var formDataValues in filteredFormDataValues) 
            {
                using var content = new StringContent(formDataValues.Value);
                postMultipartFormDataContent.Add(content, StringHelpers.Invariant($"\"{formDataValues.Key}\""));
            }
                
        }


        private static Uri GetUri(string link) 
        {
            return new Uri(StringHelpers.Invariant(link));
        }

        #endregion
    }
}