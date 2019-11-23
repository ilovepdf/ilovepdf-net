using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    ///     Watermark Horizontal Positions
    /// </summary>
    public enum WaterMarkHorizontalPositions
    {
        /// <summary>
        ///     Left
        /// </summary>
        [EnumMember(Value = "left")] Left,

        /// <summary>
        ///     Middle
        /// </summary>
        [EnumMember(Value = "center")] Middle,

        /// <summary>
        ///     Right
        /// </summary>
        [EnumMember(Value = "right")] Right
    }
}