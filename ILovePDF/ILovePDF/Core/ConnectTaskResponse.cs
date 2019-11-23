using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LovePdf.Core
{
    public sealed class ConnectTaskResponse
    {
        /// <summary>
        ///     Server
        /// </summary>
        [JsonProperty("task")]
        public String TaskId { get; set; }

        /// <summary>
        ///     Task Id
        /// </summary>
        [JsonProperty("files")]
        public Dictionary<String, String> Files { get; set; }
    }
}