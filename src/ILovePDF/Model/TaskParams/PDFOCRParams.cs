using iLovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using static iLovePdf.Model.Enums.OCRLanguage;

namespace iLovePdf.Model.TaskParams
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
        public List<OCRLanguage> OCRLanguages { get; set; }

        private void setDefaultValues()
        {
            OCRLanguages = new List<OCRLanguage>()
            {
               OCRLanguage.Eng
            };

        }
    }
}
