using iLovePdf.Helpers;
using System;
using System.Net.Http;

namespace iLovePdf.Extensions
{
    static class HttpClientExtension
    {
        /// <returns>HttpResponseMessage</returns>
        /// <inheritdoc cref="HttpClient.SendAsync(HttpRequestMessage)"/>
        public static HttpResponseMessage Send(this HttpClient client, HttpRequestMessage httpRequestMessage)
        {
            return TaskHelper.RunAsSync(client.SendAsync(httpRequestMessage));
        }

        /// <returns>HttpResponseMessage</returns>
        /// <inheritdoc cref="HttpClient.GetAsync(Uri)"/>
        public static HttpResponseMessage Get(this HttpClient client, Uri uri)
        {
            return TaskHelper.RunAsSync(client.GetAsync(uri));
        }

        /// <returns>HttpResponseMessage</returns>
        /// <inheritdoc cref="HttpClient.PostAsync(Uri, HttpContent)"/>
        public static HttpResponseMessage Post(this HttpClient client, Uri uri, HttpContent httpContent)
        {
            return TaskHelper.RunAsSync(client.PostAsync(uri, httpContent));
        }

        /// <returns>HttpResponseMessage</returns>
        /// <inheritdoc cref="HttpClient.PutAsync(string, HttpContent)"/>
        public static HttpResponseMessage Put(this HttpClient client, Uri uri, HttpContent httpContent)
        {
            return TaskHelper.RunAsSync(client.PostAsync(uri, httpContent));
        }

        /// <returns>HttpResponseMessage</returns>
        /// <inheritdoc cref="HttpClient.DeleteAsync(Uri)"/>
        public static HttpResponseMessage Delete(this HttpClient client, Uri uri)
        {
            return TaskHelper.RunAsSync(client.DeleteAsync(uri));
        }
    }
}