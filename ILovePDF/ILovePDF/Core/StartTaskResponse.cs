using Newtonsoft.Json;

namespace LovePdf.Core
{
    /// <summary>
    /// Start Task Response
    /// </summary>
    public class StartTaskResponse
    {
        /// <summary>
        /// Server
        /// </summary>
        [JsonProperty("server")]
        public string Server { get; set; }
        /// <summary>
        /// Task Id
        /// </summary>
        [JsonProperty("task")]
        public string TaskId { get; set; }
    }
}
