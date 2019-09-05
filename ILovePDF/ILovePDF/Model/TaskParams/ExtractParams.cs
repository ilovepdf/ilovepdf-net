using Newtonsoft.Json;

namespace LovePdf.Model.TaskParams
{
    public class ExtractParams : BaseParams
    {
        /// <summary>
        /// Detailed
        /// </summary>
        [JsonProperty("detailed")]
        public bool Detailed { get; set; }

        /// <summary>
        /// Detailed
        /// </summary>
        [JsonProperty("by_word")]
        public bool ByWord { get; set; }
    }
}
