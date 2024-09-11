using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    ///     Orientations
    /// </summary>
    public enum Orientations
    {
        /// <summary>
        ///     Portrait
        /// </summary>
        [EnumMember(Value = "portrait")] Portrait,

        /// <summary>
        ///     Landscape
        /// </summary>
        [EnumMember(Value = "landscape")] Landscape
    }
}