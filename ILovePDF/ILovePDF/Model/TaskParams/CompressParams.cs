using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// Compress Params
    /// </summary>
    public class CompressParams : BaseParams
    {
        /// <summary>
        /// Compression Level
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("compression_level")]
        public CompressionLevels CompressionLevel { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CompressParams()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            CompressionLevel = CompressionLevels.Recommended;
        }
    }
}
