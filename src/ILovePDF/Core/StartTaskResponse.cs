using System;
using Newtonsoft.Json;

namespace LovePdf.Core
{
    /// <summary>
    ///     Start Task Response
    /// </summary>
    public class StartTaskResponse
    {
        /// <summary>
        ///     Server
        /// </summary>
        [JsonProperty("server")]
        public String Server { get; set; }

        /// <summary>
        ///     Task Id
        /// </summary>
        [JsonProperty("task")]
        public String TaskId { get; set; }
    }
}