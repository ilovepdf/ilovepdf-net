using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;
using System;
using System.Collections.Generic;
using LovePdf.Core.Sign;
using LovePdf.Model.TaskParams.Sign;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// Try to sign PDFs
    /// </summary>
    public class SignTask : LovePdfTask
    {
        private static SignTask _instance;
        public static SignTask Instance => _instance ?? (_instance = new SignTask());
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.Sign);

        /// <inheritdoc cref="RequestHelper.CreateSignatureRequestAsync"/>
        public async System.Threading.Tasks.Task<SignatureResponse> RequestSignatureAsync(SignParams parameters)
        {
            var signatureResponse = await RequestHelper.Instance
                .CreateSignatureRequestAsync(ServerUrl, TaskId, Files, ToolName, parameters);

            // Remove files after each task execute. To prevent processing same files.
            Files.Clear();

            return signatureResponse;
        }

        /// <inheritdoc cref="RequestHelper.DownloadAuditAsync"/>
        public System.Threading.Tasks.Task<string> DownloadAuditFileAsync(string tokenRequester, string destinationPath)
        {
            return RequestHelper.Instance.DownloadAuditAsync(ServerUrl, tokenRequester, destinationPath);
        }

        /// <inheritdoc cref="RequestHelper.DownloadOriginalFilesAsync"/>
        public System.Threading.Tasks.Task<string> DownloadOriginalFilesAsync(string tokenRequester, string destinationPath)
        {
            return RequestHelper.Instance.DownloadOriginalFilesAsync(ServerUrl, tokenRequester, destinationPath);
        }

        /// <inheritdoc cref="RequestHelper.DownloadSignedFilesAsync"/>
        public System.Threading.Tasks.Task<string> DownloadSignedFilesAsync(string tokenRequester, string destinationPath)
        {
            return RequestHelper.Instance.DownloadSignedFilesAsync(ServerUrl, tokenRequester, destinationPath);
        }

        /// <inheritdoc cref="RequestHelper.FixReceiverEmailAsync"/>
        public System.Threading.Tasks.Task<ReceiverInfoResponse> FixReceiverEmailAsync(string receiverTokenRequester, string email)
        {
            return RequestHelper.Instance.FixReceiverEmailAsync(ServerUrl, receiverTokenRequester, email);
        }

        /// <inheritdoc cref="RequestHelper.FixSignerPhoneAsync"/>
        public System.Threading.Tasks.Task<ReceiverInfoResponse> FixSignerPhoneAsync(string receiverTokenRequester, string phone)
        {
            return RequestHelper.Instance.FixSignerPhoneAsync(ServerUrl, receiverTokenRequester, phone);
        }

        /// <inheritdoc cref="RequestHelper.GetReceiverInfoAsync"/>
        public System.Threading.Tasks.Task<ReceiverInfoResponse> GetReceiverInfoAsync(string receiverTokenRequester)
        {
            return RequestHelper.Instance.GetReceiverInfoAsync(ServerUrl, receiverTokenRequester);
        }

        /// <inheritdoc cref="RequestHelper.GetSignatureStatusAsync"/>
        public System.Threading.Tasks.Task<SignatureResponse> GetSignatureStatusAsync(string tokenRequester)
        {
            return RequestHelper.Instance.GetSignatureStatusAsync(ServerUrl, tokenRequester);
        }

        /// <inheritdoc cref="RequestHelper.IncreaseExpirationDaysAsync"/>
        public System.Threading.Tasks.Task<SignatureResponse> IncreaseExpirationDaysAsync(string tokenRequester, int days)
        {
            return RequestHelper.Instance.IncreaseExpirationDaysAsync(ServerUrl, tokenRequester, days.ToString(CultureInfo.InvariantCulture));
        }

        /// <inheritdoc cref="RequestHelper.ListSignaturesAsync"/>
        public System.Threading.Tasks.Task<List<SignatureResponse>> GetSignaturesAsync(ListRequest request = null)
        {
            if (request == null)
            {
                request = new ListRequest();
            }

            return RequestHelper.Instance.ListSignaturesAsync(ServerUrl, request);
        }

        /// <inheritdoc cref="RequestHelper.VoidSignatureAsync"/>
        public System.Threading.Tasks.Task<SignatureResponse> VoidSignatureAsync(string tokenRequester)
        {
            return RequestHelper.Instance.VoidSignatureAsync(ServerUrl, tokenRequester);
        }

        public System.Threading.Tasks.Task<ReceiverInfoResponse> SendRemindersAsync(string tokenRequester)
        {
            return RequestHelper.Instance.SendRemindersAsync(ServerUrl, tokenRequester);
        }

        /// <summary>
        ///     Upload file to the ILovePdf server from local drive.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="taskId">if no task provided will be used last one from create task method.</param>
        /// <param name="serverUrl"></param>
        /// <returns>Server file name</returns>
        [SuppressMessage("Microsoft.Design", "CA1057:StringUriOverloadsCallSystemUriOverloads")]
        public UploadTaskResponse AddFileLogo(Uri path, String taskId, Uri serverUrl)
        {
            return AddFileLogo(path, taskId, serverUrl, Rotate.Degrees0);
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
        public UploadTaskResponse AddFileLogo(Uri cloudLink, String taskId, Uri serverUrl, Rotate rotate)
        {
            if (cloudLink == null)
                throw new ArgumentException("cannot be null", nameof(cloudLink));

            var requestTaskId = String.IsNullOrWhiteSpace(taskId) ? TaskId : taskId;

            var response = RequestHelper.Instance.UploadFile(serverUrl, cloudLink, requestTaskId);

            //TODO check filename
            var fileName = Path.GetFileName(cloudLink.AbsoluteUri);

            if (string.IsNullOrEmpty(fileName))
            {
                var host = cloudLink.Host;
                if (host.Contains('.'))
                {
                    var parts = cloudLink.Host.Split('.');
                    fileName = parts[parts.Length - 2];
                }
                else
                {
                    fileName = host;
                }
            }

            return response;
        }

        #region Unsupported methods 

        // TODO: Temporary solution to hide unsupported methods, base class need to be refactored

        [Obsolete("This is not supported in this class.", true)]
        public new ExecuteTaskResponse Process(BaseParams parameters) => throw new NotImplementedException("Don't use!!");

        [Obsolete("This is not supported in this class.", true)]
        public new void DownloadFile() => throw new NotImplementedException("Don't use!!");

        [Obsolete("This is not supported in this class.", true)]
        public new void DownloadFile(String destination) => throw new NotImplementedException("Don't use!!");

        [Obsolete("This is not supported in this class.", true)]
        public new void DownloadFile(String destination, String taskId) => throw new NotImplementedException("Don't use!!");

        [Obsolete("This is not supported in this class.", true)]
        public new System.Threading.Tasks.Task<Byte[]> DownloadFileAsByteArrayAsync() => throw new NotImplementedException("Don't use!!");

        [Obsolete("This is not supported in this class.", true)]
        public new System.Threading.Tasks.Task<Byte[]> DownloadFileAsByteArrayAsync(String taskId) => throw new NotImplementedException("Don't use!!");

        #endregion
    }
}