using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    ///     Conformance Values for Pdf Versions
    /// </summary>
    public enum ConformanceValues
    {
        /// <summary>
        ///     PdfA1B
        /// </summary>
        [EnumMember(Value = "pdfa-1b")] PdfA1B,

        /// <summary>
        ///     PdfA1A
        /// </summary>
        [EnumMember(Value = "pdfa-1a")] PdfA1A,

        /// <summary>
        ///     PdfA2B
        /// </summary>
        [EnumMember(Value = "pdfa-2b")] PdfA2B,

        /// <summary>
        ///     PdfA2U
        /// </summary>
        [EnumMember(Value = "pdfa-2u")] PdfA2U,

        /// <summary>
        ///     PdfA2A
        /// </summary>
        [EnumMember(Value = "pdfa-2a")] PdfA2A,

        /// <summary>
        ///     PdfA3B
        /// </summary>
        [EnumMember(Value = "pdfa-3b")] PdfA3B,

        /// <summary>
        ///     PdfA3U
        /// </summary>
        [EnumMember(Value = "pdfa-3u")] PdfA3U,

        /// <summary>
        ///     PdfA3A
        /// </summary>
        [EnumMember(Value = "pdfa-3a")] PdfA3A
    }
}