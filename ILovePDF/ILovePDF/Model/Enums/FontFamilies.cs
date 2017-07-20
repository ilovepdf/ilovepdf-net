using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Font Families to choose from
    /// </summary>
    public enum FontFamilies
    {
        /// <summary>
        /// Arial
        /// </summary>
        [EnumMember(Value = "Arial")]
        Arial,

        /// <summary>
        /// Courier
        /// </summary>
        [EnumMember(Value = "Courier")]
        Courier,

        /// <summary>
        /// Times new roman
        /// </summary>
        [EnumMember(Value = "Times New Roman")]
        TimesNewRoman,

        /// <summary>
        /// Verdana
        /// </summary>
        [EnumMember(Value = "Verdana")]
        Verdana,

        /// <summary>
        /// Comic Sans MS
        /// </summary>
        [EnumMember(Value = "Comic Sans MS")]
        ComicSansMS,

        /// <summary>
        /// Wen Quan Yi Zen Hei
        /// </summary>
        [EnumMember(Value = "WenQuanYi Zen Hei")]
        WenQuanYIZenHei,

        /// <summary>
        /// Arial Unicode Ms
        /// </summary>
        [EnumMember(Value = "Arial Unicode MS")]
        ArialUnicodeMS,

        /// <summary>
        /// Lohit Marathi
        /// </summary>
        [EnumMember(Value = "Lohit Marathi")]
        LohitMarathi,

        /// <summary>
        /// Lohit Devanagari
        /// </summary>
        [EnumMember(Value = "Lohit Devanagari")]
        LohitDevanagari
    }
}
