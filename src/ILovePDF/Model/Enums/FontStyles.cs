using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    ///     Font Styles
    /// </summary>
    public enum FontStyles
    {
        /// <summary>
        ///     Bold
        /// </summary>
        [EnumMember(Value = "bold")] Bold,

        /// <summary>
        ///     Italic
        /// </summary>
        [EnumMember(Value = "italic")] Italic
    }
}