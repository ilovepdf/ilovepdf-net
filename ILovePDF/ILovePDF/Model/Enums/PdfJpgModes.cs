using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Pdf To Jpg Modes
    /// </summary>
    public enum PdfToJpgModes
    {
        /// <summary>
        /// Save each page to a Jpg
        /// </summary>
        [EnumMember(Value = "pages")]
        Pages,

        /// <summary>
        /// Extract images from Pdf
        /// </summary>
        [EnumMember(Value = "extract")]
        Extract
    }
}
