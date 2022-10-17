using System;
using Newtonsoft.Json;

namespace LovePdf.Core
{
    /// <summary>
    ///     Upload Task Response
    /// </summary>
    public class UploadTaskResponse
    {
        /// <summary>
        ///     Server file name
        /// </summary>
        [JsonProperty("server_filename")]
        public String ServerFileName { get; set; }
    }
}