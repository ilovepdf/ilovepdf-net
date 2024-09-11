using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
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
        [EnumMember(Value = "low")] Low,

        /// <summary>
        ///     SuperLow
        /// </summary>
        [EnumMember(Value = "superlow")] SuperLow
    }
}