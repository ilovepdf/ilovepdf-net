using Newtonsoft.Json;

namespace ILovePDF.DTO
{
    public class StartTaskResponse
    {
        [JsonProperty("server")]
        public string Server { get; set; }
        [JsonProperty("task")]
        public string TaskId { get; set; }
    }
}
