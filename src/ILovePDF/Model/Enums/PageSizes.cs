using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    ///     Page Sizes
    /// </summary>
    public enum PageSizes
    {
        /// <summary>
        ///     Fit
        /// </summary>
        [EnumMember(Value = "fit")] Fit,

        /// <summary>
        ///     A4
        /// </summary>
        [EnumMember(Value = "A4")] A4,

        /// <summary>
        ///     Letter
        /// </summary>
        [EnumMember(Value = "letter")] Letter
    }
}