using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;
using System;
using System.Collections.Generic;
using LovePdf.Core.Sign;
using LovePdf.Model.TaskParams.Sign;
using System.Globalization;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// Try to sign PDFs
    /// </summary>
    public class SignTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.Sign);

        /// <inheritdoc cref="RequestHelper.CreateSignatureRequestAsync"/>
        public async System.Threading.Tasks.Task<SignatureResponse> RequestSignatureAsync(SignParams parameters)
        {
            var signatureResponse = await RequestHelper.Instance
                .CreateSignatureRequestAsync(ServerUrl, TaskId, Files, ToolName, parameters);

            //Remove files after each task execute. To prevent processing same files.
            Files.Clear();

            return signatureResponse;
        }

        /// <inheritdoc cref="RequestHelper.DownloadAuditAsync"/>
        public System.Threading.Tasks.Task DownloadAuditFileAsync(string tokenRequester, string destinationPath)
        {
            return RequestHelper.Instance.DownloadAuditAsync(ServerUrl, tokenRequester, destinationPath);
        }

        /// <inheritdoc cref="RequestHelper.DownloadOriginalFilesAsync"/>
        public System.Threading.Tasks.Task DownloadOriginalFilesAsync(string tokenRequester, string destinationPath)
        {
            return RequestHelper.Instance.DownloadOriginalFilesAsync(ServerUrl, tokenRequester, destinationPath);
        }

        /// <inheritdoc cref="RequestHelper.DownloadSignedFilesAsync"/>
        public System.Threading.Tasks.Task DownloadSignedFilesAsync(string tokenRequester, string destinationPath)
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
        public System.Threading.Tasks.Task<ReceiverInfoResponse> IncreaseExpirationDaysAsync(string tokenRequester, int days)
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