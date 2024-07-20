// Any resource can be called with an additional parameter debug.
// When sending the debug parameter equal true the resource won't
// process the request but it will output the parameters received
// by the server.
// https://developer.ilovepdf.com/docs/api-reference#testing
// To enable just uncomment this directive:
//
// #define REMOTE_API_DEBUG_ENABLED
//
using Jose;
using LovePdf.Extensions;
using LovePdf.Helpers;
using LovePdf.Model.Enums;
using LovePdf.Model.Exception;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Sign.Elements;
using LovePdf.Model.TaskParams.Sign.Signers;
using Newtonsoft.Json;
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
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace LovePdf.Core
{ 
    internal partial class RequestHelper 
    { 
        private readonly Int16 _jwtDelay = 5400;
        private byte[] _privateKey;
        private string _publicKey;

        /// <summary>
        /// Do not use this. Use "HttpClient" property instead.
        /// </summary>
        private readonly HttpClient _xHttpClient;

        private static RequestHelper _instance;

        private static readonly DateTime epoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static readonly TimeSpan jwtTolerance = TimeSpan.FromMinutes(5);

        private RequestHelper()
        {
            _xHttpClient = new HttpClient();
        }

        private string JwtToken { get; set; }

        private string EncryptKey { get; set; }

        /// <summary>
        /// Use it for making http requests.
        /// </summary>
        private HttpClient HttpClient
        {
            get
            {
                _xHttpClient.Timeout = TimeSpan.FromSeconds(30);
                AddAuthorizationHeader(_xHttpClient);
                return _xHttpClient;
            }
        }

        public static RequestHelper Instance => _instance ?? (_instance = new RequestHelper());

        #region Tasks
        public StartTaskResponse StartTask(string tool)
        {
            var link = GetUri($"{Settings.StartUrl}/{Settings.V1}/start/{tool}");

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, link))
            {
                var response = HttpClient.Send(requestMessage);
                return ProccessHttpResponse<StartTaskResponse>(response);
            }
        }

        public ConnectTaskResponse ConnectTask(string parentTaskId, string tool)
        {
            var link = GetUri($"{Settings.StartUrl}/{Settings.V1}/task/next");

            using (var multipartFormDataContent = new MultipartFormDataContent())
            {
                var formData = PrepareFormData(
                        taskId: parentTaskId,
                        tool: tool);
                SetMultiPartFormData(formData, multipartFormDataContent);

                var response = HttpClient.Post(link, multipartFormDataContent);
                return ProccessHttpResponse<ConnectTaskResponse>(response);
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
        public ExecuteTaskResponse ExecuteTask(Uri serverUrl, string taskId, List<FileModel> files, string tool,
            BaseParams parameters)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/process");

            using (var multipartFormDataContent = new MultipartFormDataContent())
            {
                var initalValues = new List<KeyValuePair<string, string>>()
                {
                    new ("task", taskId),
                    new ("tool", tool),
                    new ("v", $"net.{Settings.NetVersion}") 
                };

                SetFormDataForExecuteTask(parameters, files, initalValues, multipartFormDataContent);

                var response = HttpClient.Post(link, multipartFormDataContent);
                return ProccessHttpResponse<ExecuteTaskResponse>(response);
            }
        }

        public StatusTaskResponse CheckTaskStatus(Uri serverUrl, string taskId)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/task/{taskId}");

            var response = HttpClient.Get(link);
            return ProccessHttpResponse<StatusTaskResponse>(response);
        }

        public DeleteTaskResponse DeleteTask(Uri serverUrl, string taskId)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/task/{taskId}");

            var response = HttpClient.Delete(link);
            return ProccessHttpResponse<DeleteTaskResponse>(response);
        }

        #endregion Tasks

        #region FileManipulations
        public void Download(Uri serverUrl, string taskId, string destinationPath)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/download/{taskId}");
            TaskHelper.RunAsSync(DownloadFileAsync(link, destinationPath));
        }

        public async Task<string> DownloadFileAsync(Uri link, string destinationPath)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, link))
            {
                var response = await HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    await ProccessHttpResponseAsync(response).ConfigureAwait(false);
                } 

                response.EnsureSuccessStatusCode();

                var responseContentStream =  await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                var fileName = response.Content.Headers.ContentDisposition.FileName
                    .Replace("\"", string.Empty);

                var filePath = Path.Combine(destinationPath, fileName);
                using (var outputStream = new FileStream(
                    filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    await responseContentStream.CopyToAsync(outputStream).ConfigureAwait(false);
                }
                return filePath;
            }
        }

        public byte[] Download(string serverUrl, string taskId)
        {
            return TaskHelper.RunAsSync(DownloadAsync(new Uri(serverUrl), taskId));
        }

        public async Task<byte[]> DownloadAsync(Uri serverUrl, string taskId)
        {
            var link = $"{serverUrl}{Settings.V1}/download/{taskId}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, link))
            {
                var response = await HttpClient.SendAsync(request).ConfigureAwait(false); ;
                return await ProccessHttpResponseAsync(response).ConfigureAwait(false);
            }
        }

        public UploadTaskResponse UploadFile(Uri serverUrl, byte[] fileByteArray, string fileName, string taskId)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/upload");

            using (var multiPart = new MultipartFormDataContent())
            {
                var uploadRequest = new BaseTaskRequest();
                uploadRequest.FormData.Add("task", taskId);
                uploadRequest.FormData.Add("file", new FileParameter(fileByteArray, fileName));

                SetMultiPartFormData(uploadRequest.FormData, multiPart);

                var response = HttpClient.Post(link, multiPart);
                return ProccessHttpResponse<UploadTaskResponse>(response);
            }
        }

        public UploadTaskResponse UploadFile(Uri serverUrl, Uri url, string taskId)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/upload");

            using (var multiPart = new MultipartFormDataContent())
            {
                var request = new BaseTaskRequest();
                request.FormData.Add("cloud_file", url.AbsoluteUri);
                request.FormData.Add("task", taskId);
                SetMultiPartFormData(request.FormData, multiPart);

                var response = HttpClient.Post(link, multiPart);
                return ProccessHttpResponse<UploadTaskResponse>(response);
            }
        }

        public UploadTaskResponse UploadFile(Uri serverUrl, FileInfo file, string taskId)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/upload");

            using (Stream fs = file.OpenRead())
            using (var multipartFormData = new MultipartFormDataContent())
            {
                var uploadRequest = new BaseTaskRequest();
                uploadRequest.FormData.Add("file", new FileParameter(fs, file.Name));
                uploadRequest.FormData.Add("task", taskId);

                SetMultiPartFormData(uploadRequest.FormData, multipartFormData);

                var response = HttpClient.Post(link, multipartFormData);
               return ProccessHttpResponse<UploadTaskResponse>(response);
            }
        }

        public UploadTaskResponse UploadFileByChunk(Uri serverUrl, FileInfo fileInfo, string taskId)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/upload");

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

            UploadTaskResponse uploadTaskResponse = null;
            for (var i = 0; i < chunksToUpload.Count; i++)
            {
                using (var multipartFormDataContent = new MultipartFormDataContent())
                {
                    uploadRequest.FormData["chunk"] = i.ToString(CultureInfo.InvariantCulture);
                    uploadRequest.FormData["file"] = new FileParameter(chunksToUpload[i], fileInfo.Name);

                    SetMultiPartFormData(uploadRequest.FormData, multipartFormDataContent);

                    var response = HttpClient.Post(link, multipartFormDataContent);
                    uploadTaskResponse = ProccessHttpResponse<UploadTaskResponse>(response);
                }
            }

            return uploadTaskResponse;
        }

        public void DeleteFile(Uri serverUrl, string serverFileName, string taskId)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/upload/delete");

            using (var multipartFormDataContent = new MultipartFormDataContent())
            {
                var formData = PrepareFormData(
                        taskId: taskId,
                        serverFileName: serverFileName);
                SetMultiPartFormData(formData, multipartFormDataContent);

                var response = HttpClient.Post(link, multipartFormDataContent);
                ProccessHttpResponse<dynamic>(response);
            }
        }

        #endregion FileManipulations

        public RequestHelper SetKeys(string privateKey, string publicKey)
        {
            _privateKey = Encoding.UTF8.GetBytes(privateKey);
            _publicKey = publicKey;
            return this;
        }

        /// <summary>
        /// Set file encrypt key
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="useBuildIn"></param>
        /// <returns></returns>
        public RequestHelper SetEncryptKey(string encryptKey = "", Boolean useBuildIn = false)
        {
            //if using build in skip encryptKey param even if it is provided
            if (useBuildIn)
                encryptKey = Guid.NewGuid().ToString("n").Substring(0, 32);

            if (string.IsNullOrWhiteSpace(encryptKey)) return this;

            if (encryptKey.Length != 16 && encryptKey.Length != 24 && encryptKey.Length != 32)
                throw new ArgumentOutOfRangeException(nameof(encryptKey),
                    "Only keys of sizes 16, 24 or 32 are supported.");

            EncryptKey = encryptKey;

            return this;
        }

        #region Help methods

        private static Dictionary<string, object> PrepareFormData(string taskId = null, string serverFileName = null, string tool = null)
        {
            var formData = new Dictionary<string, object>();

            if (taskId != null)
            {
                formData.Add("task", taskId);
            }

            if (serverFileName != null)
            {
                formData.Add("server_filename", serverFileName);
            }

            if (tool != null)
            {
                formData.Add("tool", tool);
            }

            return formData;
        }

        private static T ProccessHttpResponse<T>(HttpResponseMessage response)
        {
            
            T checkType = default(T);

            if (response == null)
            {
                return checkType;
            }

            var responseContent = string.Empty;

            try
            {
                responseContent = GetContent(response);
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

        private static async Task<T> ProccessHttpResponseAsync<T>(HttpResponseMessage response)
        {
            T checkType = default(T);

            if (response == null)
            {
                return checkType;
            }

            var responseContent = string.Empty;

            try
            {
                responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }
         
        private static async Task<byte[]> ProccessHttpResponseAsync(HttpResponseMessage response)
        {
            if (response == null)
            {
                return null;
            }

            var responseContent = string.Empty;

            try
            {
                responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw ParseRequestErrors(response, responseContent, e);
            }
        }

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

        /// <summary>
        /// Set authorization header for http client
        /// </summary>
        /// <param name="httpClient"></param>
        private void AddAuthorizationHeader(HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(JwtToken) || IsExpiredJwt())
            {
                JwtToken = GetJwt();
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
        }

        /// <summary>
        /// Check if GWT token expired
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031")]
        private Boolean IsExpiredJwt()
        {
            try
            {
                JWT.Decode(JwtToken, _privateKey, JwsAlgorithm.HS256);
                var expired = epoch.AddSeconds(
                    (JObject.Parse(JWT.Payload(JwtToken))["exp"] ?? 0).Value<double>())
                    .Subtract(jwtTolerance);
                return expired < DateTime.UtcNow;
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// Generate GWT token
        /// </summary>
        /// <returns></returns>
        private string GetJwt()
        {
            var payLoad = new Dictionary<string, Object>
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
            if (!string.IsNullOrWhiteSpace(EncryptKey)) payLoad.Add("file_encryption_key", EncryptKey);

            var token = JWT.Encode(payLoad, _privateKey, JwsAlgorithm.HS256);

            return token;
        }

        private static void SetMultiPartFormData(Dictionary<string, Object> formData,
            MultipartFormDataContent multiPartFormDataContent)
        {

#if REMOTE_API_DEBUG_ENABLED  
           formData.Add("debug", "true");
#warning   RequestHelper sending the "debug" parameter equal true the iLovePdf server won't process the request but it will output the parameters received by the server. You can disable it on Core/RequestHelper.cs file by commenting REMOTE_API_DEBUG_ENABLED directive at top of file.
#endif

            foreach (var param in formData)
            {
                if (param.Value is FileParameter file)
                {
                    var uploadFile = file;

                    if (uploadFile.FileStream != null)
                    {
                        var content = new StreamContent(uploadFile.FileStream);
                        multiPartFormDataContent.Add(content, "file", uploadFile.FileName);
                    }
                    else
                    { 
                        var content = new ByteArrayContent(uploadFile.File);
                        multiPartFormDataContent.Add(content, "file", uploadFile.FileName);
                    }
                }
                else
                {
                    var content = new StringContent((string)param.Value);
                    multiPartFormDataContent.Add(content, param.Key);
                }
            }
        }

        private static void SetFormDataForExecuteTask(BaseParams @params, IReadOnlyList<FileModel> files,
            List<KeyValuePair<string, string>> initialValues, MultipartFormDataContent postMultipartFormDataContent)
        {
            if (@params != null)
            {
                initialValues.AddRange(
                    InitialValueHelper.GetInitialValues(@params, string.Empty));
            }

            if (@params is WaterMarkParams watermarkParams)
            {
                var elements = watermarkParams.Elements;
                for (var index = 0; index < elements.Count; index++)
                {
                    var element = elements[index];

                    initialValues.AddRange(
                        InitialValueHelper.GetInitialValues(element, $"elements[{index}]"));
                }
            }

            if (@params is EditParams editParams)
            {
                var elements = editParams.Elements;
                for (var index = 0; index < elements.Count; index++)
                {
                    var element = elements[index];

                    initialValues.AddRange(
                        InitialValueHelper.GetInitialValues(element, $"elements[{index}]"));

                    if (element.Coordinates != null)
                    {
                        initialValues.AddRange(
                            InitialValueHelper.GetInitialValues(element.Coordinates, $"elements[{index}][coordinates]"));
                    }

                    if (element.Dimensions != null)
                    {
                        initialValues.AddRange(
                            InitialValueHelper.GetInitialValues(element.Dimensions, $"elements[{index}][dimensions]"));
                    }
                }
            }

            if (@params is SignParams signParams)
            {
                var signers = signParams.Signers;
                for (var index = 0; index < signers.Count; index++)
                {
                    var signerItem = signers[index];

                    initialValues.AddRange(
                        InitialValueHelper.GetInitialValues(signerItem, $"signers[{index}]"));

                    if (signerItem is Signer signer)
                    {
                        for (var fileIndex = 0; fileIndex < signer.Files.Count; fileIndex++)
                        { 
                            var file = signer.Files[fileIndex];
                            initialValues.AddRange(
                                InitialValueHelper.GetInitialValues(file, $"signers[{index}][files][{fileIndex}]"));

                            for (var elementIndex = 0; elementIndex < file.Elements.Count; elementIndex++)
                            {
                                var element = file.Elements[elementIndex];
                                initialValues.AddRange(
                                    InitialValueHelper.GetInitialValues(element, $"signers[{index}][files][{fileIndex}][elements][{elementIndex}]"));
                            }
                        } 
                    } 
                }
            }

            for (var i = 0; i < files.Count; i++)
            {
                initialValues.AddItem($"files[{i}][filename]", files[i].FileName);
                initialValues.AddItem($"files[{i}][server_filename]", files[i].ServerFileName);
                initialValues.AddItem($"files[{i}][rotate]", ((int)files[i].Rotate).ToString(CultureInfo.InvariantCulture));
                initialValues.AddItem($"files[{i}][password]", files[i].Password);
            }

            var filteredFormDataValues = initialValues.Where(x => !string.IsNullOrWhiteSpace(x.Value));
            foreach (var formDataValues in filteredFormDataValues)
            {
                var content = new StringContent(formDataValues.Value);
                postMultipartFormDataContent.Add(content, StringHelpers.Invariant($"\"{formDataValues.Key}\""));
            }
        }

        private static Uri GetUri(string link)
        {
            return new Uri(StringHelpers.Invariant(link));
        }
         
        private static string GetContent(HttpResponseMessage response)
        {
            var task = response?.Content?.ReadAsStringAsync();
            if (task != null)
            {
                return TaskHelper.RunAsSync(task);
            }
            return null;
        }

        private static Exception ParseRequestErrors(HttpResponseMessage response, string responseContent,
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

                if (parsedContent.error.type == EnumExtensions.GetEnumDescription(LovePdfErrors.StartError))
                    return new SignStartException(responseContent, exception);

                if (parsedContent.error.type == EnumExtensions.GetEnumDescription(LovePdfErrors.SignatureError))
                    return new SignatureException(responseContent, exception);

                return new UndefinedException(responseContent, exception); 
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized) // 401 Unauthorized
                return new AuthenticationException(responseContent, exception);

            if (response.StatusCode == HttpStatusCode.NotFound) // 404 Not Found
                return new NotFoundException(responseContent, exception);

            if (response.StatusCode == (HttpStatusCode)429) // 429 Too many Requests
                return new TooManyRequestsException(responseContent, exception);

            if ((Int32)response.StatusCode >= 500 && (Int32)response.StatusCode < 600) // 5xx Server Errors
                return new ServerErrorException(responseContent, exception);

            return exception;
        } 

        #endregion
    }
}