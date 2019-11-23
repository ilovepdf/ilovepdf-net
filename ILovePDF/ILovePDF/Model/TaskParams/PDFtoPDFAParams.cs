using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     Pdf to PdfA Params
    /// </summary>
    public class PdfToPdfAParams : BaseParams
    {
        /// <summary>
        ///     Pdf to PdfA Params Constructor
        /// </summary>
        public PdfToPdfAParams()
        {
            setDefaultValues();
        }

        /// <summary>
        ///     Accepted values in ConformanceValues (pdfa-1b, pdfa-1a, pdfa-2b, pdfa-2u, pdfa-2a, pdfa-3b, pdfa-3u, pdfa-3a)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("conformance")]
        public ConformanceValues Conformance { get; set; }

        private void setDefaultValues()
        {
            Conformance = ConformanceValues.PdfA1A;
        }
    }
}