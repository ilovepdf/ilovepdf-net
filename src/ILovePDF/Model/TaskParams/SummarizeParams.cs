using Newtonsoft.Json;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    /// Parameters for Summarize task
    /// </summary>
    public class SummarizeParams : BaseParams
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("output_format")]
        public string OutputFormat { get; set; }
    }
}
