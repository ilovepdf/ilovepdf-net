using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     PDFOCRParams Params
    /// </summary>
    public class PDFOCRParams : BaseParams
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public PDFOCRParams()
        {
            setDefaultValues();
        }

        /// <summary>
        ///     Compression Level
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("ocr_languages")]
        public CompressionLevels CompressionLevel { get; set; }

        private void setDefaultValues()
        {
            CompressionLevel = CompressionLevels.Recommended;
        }
    }
}
