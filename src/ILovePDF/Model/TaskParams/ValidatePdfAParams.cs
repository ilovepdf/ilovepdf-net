﻿using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     Validate PdfA Params
    /// </summary>
    public class ValidatePdfAParams : BaseParams
    {
        /// <summary>
        ///     Validate PdfA Params Constructor
        /// </summary>
        public ValidatePdfAParams()
        {
            setDefaultValues();
        }

        /// <summary>
        ///     Validate PdfA Params Constructor
        /// </summary>
        /// <param name="conformance">conformance level for pdf file</param>
        public ValidatePdfAParams(ConformanceValues conformance)
        {
            Conformance = conformance;
        }

        /// <summary>
        ///     Accepted values in ConformanceValues (pdfa-1b, pdfa-1a, pdfa-2b, pdfa-2u, pdfa-2a, pdfa-3b, pdfa-3u, pdfa-3a)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("conformance")]
        public ConformanceValues Conformance { get; set; }


        private void setDefaultValues()
        {
            Conformance = ConformanceValues.PdfA1B;
        }
    }
}