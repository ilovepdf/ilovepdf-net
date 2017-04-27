using ILovePDF.Model.Enum.Params;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ILovePDF.Model.TaskParams
{
    public class CompressParams : BaseParams
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("compression_level")]
        public CompressionLevels CompressionLevel { get; set; }

        public CompressParams()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            CompressionLevel = CompressionLevels.recommended;
        }
    }
}
