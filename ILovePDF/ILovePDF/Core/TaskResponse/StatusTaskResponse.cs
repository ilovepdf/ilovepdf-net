using Newtonsoft.Json;

namespace ILovePDF.Core.TaskResponse
{
    public class StatusTaskResponse
    {
        [JsonProperty("status")]
        public string TaskStatus { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }
    }
}
