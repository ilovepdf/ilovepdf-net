using Newtonsoft.Json;

namespace ILovePDF.Model.TaskParams
{
    public class PDFtoPDFAParams : BaseParams
    {
        /// <summary>
        /// Accepted values pdfa-1b, pdfa-1a, pdfa-2b, pdfa-2u, pdfa-2a, pdfa-3b, pdfa-3u, pdfa-3a
        /// </summary>
        [JsonProperty("conformance")]
        public string Conformance { get; set; }

        public PDFtoPDFAParams()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Conformance = "pdfa-1a";
        }
    }
}
