using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Layer
    /// </summary>
    public enum Layer
    {
        /// <summary>
        /// Above
        /// </summary>
        [EnumMember(Value = "above")]
        Above,

        /// <summary>
        /// Below
        /// </summary>
        [EnumMember(Value = "below")]
        Below,
    }
}
