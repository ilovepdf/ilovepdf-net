using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    /// Element Types
    /// </summary>
    public enum ElementTypes
    {
        /// <summary>
        /// Bottom
        /// </summary>
        [EnumMember(Value = "bottom")] Bottom,

        /// <summary>
        /// Text
        /// </summary>
        [EnumMember(Value = "text")] Text,

        /// <summary>
        /// Image
        /// </summary>
        [EnumMember(Value = "image")] Image,

        /// <summary>
        /// SVG
        /// </summary>
        [EnumMember(Value = "svg")] SVG,
    }
}