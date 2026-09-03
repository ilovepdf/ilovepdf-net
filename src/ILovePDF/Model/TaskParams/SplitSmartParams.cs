using Newtonsoft.Json;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    /// Parameters for SplitSmart task
    /// </summary>
    public class SplitSmartParams : BaseParams
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }
    }
}
