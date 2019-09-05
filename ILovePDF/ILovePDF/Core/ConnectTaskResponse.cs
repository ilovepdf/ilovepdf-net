using System.Collections.Generic;
using Newtonsoft.Json;

namespace LovePdf.Core
{
    public sealed class ConnectTaskResponse
    {
        /// <summary>
        /// Server
        /// </summary>
        [JsonProperty("task")]
        public string TaskId { get; set; }

        /// <summary>
        /// Task Id
        /// </summary>
        [JsonProperty("files")]
        public Dictionary<string, string> Files { get; set; }
    }
}
