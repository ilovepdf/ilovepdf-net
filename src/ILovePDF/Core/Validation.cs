using System;
using iLovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iLovePdf.Core
{
    /// <summary>
    ///     Validation
    /// </summary>
    public class Validation
    {
        /// <summary>
        ///     Rotation
        /// </summary>
        [JsonProperty("server_filename")]
        public String ServerFileName { get; set; }

        /// <summary>
        ///     Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ValidationStatus Status { get; set; }
    }
}