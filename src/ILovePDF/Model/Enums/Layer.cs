using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    ///     Layer
    /// </summary>
    public enum Layer
    {
        /// <summary>
        ///     Above
        /// </summary>
        [EnumMember(Value = "above")] Above,

        /// <summary>
        ///     Below
        /// </summary>
        [EnumMember(Value = "below")] Below
    }
}