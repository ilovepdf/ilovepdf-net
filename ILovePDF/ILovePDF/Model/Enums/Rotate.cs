using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Rotate Options
    /// </summary>
    public enum Rotate
    {
        /// <summary>
        /// 0 degrees
        /// </summary>
        [EnumMember(Value = "_0")]
        Degrees0 = 0,

        /// <summary>
        /// 90 degrees
        /// </summary>
        [EnumMember(Value = "_90")]
        Degrees90 = 90,

        /// <summary>
        /// 180 degrees
        /// </summary>
        [EnumMember(Value = "_180")]
        Degrees180 = 180,

        /// <summary>
        /// 270 degrees
        /// </summary>
        [EnumMember(Value = "_270")]
        Degrees270 = 270
    }
}
