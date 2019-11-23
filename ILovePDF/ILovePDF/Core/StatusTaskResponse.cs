using System;
using Newtonsoft.Json;

namespace LovePdf.Core
{
    /// <summary>
    ///     Status Task Response
    /// </summary>
    public class StatusTaskResponse
    {
        /// <summary>
        ///     Task Status
        /// </summary>
        [JsonProperty("status")]
        public String TaskStatus { get; set; }

        /// <summary>
        ///     Status Message
        /// </summary>
        [JsonProperty("status_message")]
        public String StatusMessage { get; set; }
    }
}