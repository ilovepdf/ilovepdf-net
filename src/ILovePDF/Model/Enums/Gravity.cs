using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    ///     Watermark Gravity
    /// </summary>
    public enum Gravity
    {
        /// <summary>
        ///     North West
        /// </summary>
        [EnumMember(Value = "NorthWest")] NorthWest,

        /// <summary>
        ///     North
        /// </summary>
        [EnumMember(Value = "North")] North,

        /// <summary>
        ///     North East
        /// </summary>
        [EnumMember(Value = "NorthEast")] NorthEast,

        /// <summary>
        ///     West
        /// </summary>
        [EnumMember(Value = "West")] West,

        /// <summary>
        ///     Center
        /// </summary>
        [EnumMember(Value = "Center")] Center,

        /// <summary>
        ///     East
        /// </summary>
        [EnumMember(Value = "East")] East,

        /// <summary>
        ///     South West
        /// </summary>
        [EnumMember(Value = "SouthWest")] SouthWest,

        /// <summary>
        ///     NorthWest
        /// </summary>
        [EnumMember(Value = "South")] South,

        /// <summary>
        ///     South East
        /// </summary>
        [EnumMember(Value = "SouthEast")] SouthEast
    }
}