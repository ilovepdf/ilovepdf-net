using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    /// Font Styles
    /// </summary>
    public enum TextFontStyles
    {
        /// <summary>
        /// Regular
        /// </summary>
        [EnumMember(Value = "Regular")] Regular,

        /// <summary>
        /// Bold
        /// </summary>
        [EnumMember(Value = "Bold")] Bold,

        /// <summary>
        /// Italic
        /// </summary>
        [EnumMember(Value = "Italic")] Italic,

        /// <summary>
        /// Bold italic
        /// </summary>
        [EnumMember(Value = "Bold italic")] BoldItalic
    }
}