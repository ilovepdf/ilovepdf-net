using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LovePdf.Extensions
{
    static class HttpClientExtension
    {
        /// <summary>
        /// Send an HTTP request as an Synchronous operation.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        public static HttpResponseMessage Send(this HttpClient client, HttpRequestMessage httpRequestMessage)
        {
            var callDownloadTask = Task.Run(() => client.SendAsync(httpRequestMessage));
            callDownloadTask.Wait();
            return callDownloadTask.Result;
        }
    }
}
