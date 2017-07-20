using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Horizontal Positions
    /// </summary>
    public enum HorizontalPositions
    {
        /// <summary>
        /// Left
        /// </summary>
        [EnumMember(Value = "left")]
        Left,

        /// <summary>
        /// Middle
        /// </summary>
        [EnumMember(Value = "middle")]
        Middle,

        /// <summary>
        /// Right
        /// </summary>
        [EnumMember(Value = "right")]
        Right
    }
}
