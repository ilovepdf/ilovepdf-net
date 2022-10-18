using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Text alignment.   
    /// </summary>
    public enum TextAligments
    {
        /// <summary>
        /// Left
        /// </summary>
        [EnumMember(Value = "left")] Left,

        /// <summary>
        /// Center
        /// </summary>
        [EnumMember(Value = "center")] Center,

        /// <summary>
        /// Right
        /// </summary>
        [EnumMember(Value = "right")] Right
    }
}