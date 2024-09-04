using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    ///     Split Modes
    /// </summary>
    public enum SplitModes
    {
        /// <summary>
        ///     Ranges
        /// </summary>
        [EnumMember(Value = "ranges")] Ranges,

        /// <summary>
        ///     Fixed Rage
        /// </summary>
        [EnumMember(Value = "fixed_range")] FixedRange,

        /// <summary>
        ///     Remove Pages
        /// </summary>
        [EnumMember(Value = "remove_pages")] RemovePages
    }
}