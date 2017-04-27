using Newtonsoft.Json;

namespace ILovePDF.Core.TaskResponse
{
    public class UploadTaskResponse
    {
        [JsonProperty("server_filename")]
        public string ServerFileName { get; set; }
    }
}
