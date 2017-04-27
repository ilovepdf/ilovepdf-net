
using Newtonsoft.Json;

namespace ILovePDF.Core.TaskResponse
{
    public class DeleteTaskResponse
    {
        [JsonProperty("upload_status")]
        public string Upload_Status { get; set; }
    }
}
