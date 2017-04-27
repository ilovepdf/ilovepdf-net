using ILovePDF.Model.Enum.Params;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ILovePDF.Model.TaskParams
{
    public class PDFtoJPGParams : BaseParams
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("pdfjpg_mode")]
        public PdfJpgModes PdfJpgMode { get; set; }

        public PDFtoJPGParams()
        {
            SetDefaultValues();
        }
        private void SetDefaultValues()
        {
            PdfJpgMode = PdfJpgModes.pages;
        }
    }
}
