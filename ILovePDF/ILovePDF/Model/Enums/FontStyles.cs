using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Font Styles
    /// </summary>
    public enum FontStyles
    {
        /// <summary>
        /// Bold
        /// </summary>
        [EnumMember(Value = "bold")]
        Bold,

        /// <summary>
        /// Italic
        /// </summary>
        [EnumMember(Value = "italic")]
        Italic
    }
}
