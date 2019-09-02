using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// WaterMark Modes
    /// </summary>
    public enum WaterMarkModes
    {
        /// <summary>
        /// Text
        /// </summary>
        [EnumMember(Value = "text")]
        Text,

        /// <summary>
        /// Image
        /// </summary>
        [EnumMember(Value = "image")]
        Image,

        /// <summary>
        /// Image
        /// </summary>
        [EnumMember(Value = "multi")]
        Multi
    }
}
