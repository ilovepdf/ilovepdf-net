using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    ///     Compression Levels
    /// </summary>
    public enum CompressionLevels
    {
        /// <summary>
        ///     Extreme
        /// </summary>
        [EnumMember(Value = "extreme")] Extreme,

        /// <summary>
        ///     Recommended
        /// </summary>
        [EnumMember(Value = "recommended")] Recommended,

        /// <summary>
        ///     Low
        /// </summary>
        [EnumMember(Value = "low")] Low
    }
}