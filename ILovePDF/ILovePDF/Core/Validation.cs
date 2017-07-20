using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Core
{
    /// <summary>
    /// Validation
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Rotation
        /// </summary>
        [JsonProperty("server_filename")]
        public string ServerFileName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ValidationStatus Status { get; set; }

    }
}