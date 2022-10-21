﻿using LovePdf.Core.Sign;
using LovePdf.Extensions;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Sign;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LovePdf.Core
{
    internal partial class RequestHelper
    {
        /// <summary>
        /// This is used to create a signature request.
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="taskId"></param>
        /// <param name="files"></param>
        /// <param name="tool"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<SignatureResponse> CreateSignatureRequestAsync(Uri serverUrl, string taskId, List<FileModel> files, string tool,
            BaseParams parameters)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/signature");

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
                return ProccessHttpResponseAsync<SignatureResponse>(response);
            }
        }

        /// <summary>
        /// List all created signature requests
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<SignatureResponse>> ListSignaturesAsync(Uri serverUrl, ListRequest request) =>
           GetAsync<List<SignatureResponse>>(serverUrl, $"list?page={request.Page}&per-page={request.PerPage}");

        /// <summary>
        /// Get Signature status
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="tokenRequester"></param>
        /// <returns></returns>
        public Task<SignatureResponse> GetSignatureStatusAsync(Uri serverUrl, string tokenRequester) =>
            GetAsync<SignatureResponse>(serverUrl, $"requesterview/{tokenRequester}");

        /// <summary>
        /// Get Receiver info
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="receiverTokenRequester">Receiver TokenRequester</param>
        /// <returns></returns>
        public Task<ReceiverInfoResponse> GetReceiverInfoAsync(Uri serverUrl, string receiverTokenRequester) =>
            GetAsync<ReceiverInfoResponse>(serverUrl, $"receiver/info/{receiverTokenRequester}");

        /// <summary>
        /// Use this to correct the receiver's email address in the event 
        /// that the email was not delivered to a valid email address.
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="receiverTokenRequester">Receiver TokenRequester</param>
        /// <param name="email">New valid email for the receiver</param>
        /// <returns></returns>
        public Task<ReceiverInfoResponse> FixReceiverEmailAsync(Uri serverUrl, string receiverTokenRequester, string email) =>
           PutAsync<ReceiverInfoResponse>(serverUrl, $"receiver/fix-email/{receiverTokenRequester}", nameof(email), email);

        /// <summary>
        /// Use this to correct the signers's mobile number in the event that the SMS was not delivered to a valid mobile number.
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="receiverTokenRequester">Receiver TokenRequester</param>
        /// <param name="phone">New valid mobile number for the signer.</param>
        /// <returns></returns>
        public Task<ReceiverInfoResponse> FixSignerPhoneAsync(Uri serverUrl, string receiverTokenRequester, string phone) =>
            PutAsync<ReceiverInfoResponse>(serverUrl, $"signer/fix-phone/{receiverTokenRequester}", nameof(phone), phone);

        /// <summary>
        /// It voids a signature that it is currently in progress. Once voided, it will not be accessible for any receiver of the request.
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="tokenRequester">TokenRequester</param>
        /// <returns></returns>
        public Task<SignatureResponse> VoidSignatureAsync(Uri serverUrl, string tokenRequester) =>
            PutAsync<SignatureResponse>(serverUrl, $"void/{tokenRequester}");

        /// <summary>
        /// Increase the number of days in order to prevent the request from 
        /// expiring and give receivers extra time to perform remaining actions.
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="tokenRequester"></param>
        /// <param name="days"> The number of days to increase; a minimum of 1 and a maximum of 130.
        /// The signature cannot have an expiration date bigger than 130 days from now.
        /// </param>
        /// <returns></returns>
        public Task<ReceiverInfoResponse> IncreaseExpirationDaysAsync(Uri serverUrl, string tokenRequester, string days) =>
           PutAsync<ReceiverInfoResponse>(serverUrl, $"increase-expiration-days/{tokenRequester}", nameof(days), days);

        /// <summary>
        /// It downloads the audit PDF file.
        /// <para>NOTE: The signature request status must be completed.</para>
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="tokenRequester"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public Task DownloadAuditAsync(Uri serverUrl, string tokenRequester, string destinationPath) =>
            DownloadAsync(serverUrl, $"{tokenRequester}/download-audit", destinationPath);

        /// <summary>
        /// It downloads the original files.
        /// It returns a PDF file if a single file was uploaded.
        /// Otherwise a zip file with all uploaded files is returned.
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="tokenRequester"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public Task DownloadOriginalFilesAsync(Uri serverUrl, string tokenRequester, string destinationPath) =>
            DownloadAsync(serverUrl, $"{tokenRequester}/download-original", destinationPath);

        /// <summary>
        /// It downloads the signed files.
        /// It returns a PDF file if a single file was uploaded.Otherwise 
        /// a zip file with all uploaded files is returned.
        /// 
        /// <para>
        /// NOTE:  If the Signature request status is different from completed, 
        /// the call to this API will not return any files and it will yield an 
        /// HTTP status code 400
        /// </para>
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="tokenRequester"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public Task DownloadSignedFilesAsync(Uri serverUrl, string tokenRequester, string destinationPath) =>
            DownloadAsync(serverUrl, $"{tokenRequester}/download-signed", destinationPath);

        #region PrivateHttpHelpers

        private Task DownloadAsync(Uri serverUrl, string path, string destinationPath)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/${path}");
            return DownloadFileAsync(link, destinationPath);
        }

        private async Task<T> GetAsync<T>(Uri serverUrl, string path)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/${path}");
            var response = await HttpClient.GetAsync(link);
            return await ProccessHttpResponseAsync<T>(response);
        }

        private async Task<T> PutAsync<T>(Uri serverUrl, string path, string paramName = null, string paramValue = null)
        {
            var link = GetUri($"{serverUrl}{Settings.V1}/${path}");
            var multipartFormDataContent = new MultipartFormDataContent();

            if (paramName != null)
            {
                var content = new StringContent(paramValue);
                multipartFormDataContent.Add(content, StringHelpers.Invariant($"\"{paramName}\""));
            }

            var response = await HttpClient.PutAsync(link, multipartFormDataContent);
            return await ProccessHttpResponseAsync<T>(response);
        }

        #endregion PrivateHttpHelpers
    }
}
