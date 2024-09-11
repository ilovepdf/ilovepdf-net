using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    ///     Watermark vertical positions
    /// </summary>
    public enum WaterMarkVerticalPositions
    {
        /// <summary>
        ///     Bottom
        /// </summary>
        [EnumMember(Value = "bottom")] Bottom,

        /// <summary>
        ///     Top
        /// </summary>
        [EnumMember(Value = "top")] Top,

        /// <summary>
        ///     Center
        /// </summary>
        [EnumMember(Value = "middle")] Center
    }
}