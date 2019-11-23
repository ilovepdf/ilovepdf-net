using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     Compress Params
    /// </summary>
    public class CompressParams : BaseParams
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public CompressParams()
        {
            setDefaultValues();
        }

        /// <summary>
        ///     Compression Level
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("compression_level")]
        public CompressionLevels CompressionLevel { get; set; }

        private void setDefaultValues()
        {
            CompressionLevel = CompressionLevels.Recommended;
        }
    }
}