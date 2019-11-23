using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     Pdf To Jpg Params
    /// </summary>
    public class PdftoJpgParams : BaseParams
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public PdftoJpgParams()
        {
            setDefaultValues();
        }

        /// <summary>
        ///     Pdf To Jpg Mode
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("pdfjpg_mode")]
        public PdfToJpgModes PdfJpgMode { get; set; }

        private void setDefaultValues()
        {
            PdfJpgMode = PdfToJpgModes.Pages;
        }
    }
}