using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Orientations
    /// </summary>
    public enum Orientations
    {
        /// <summary>
        /// Portrait
        /// </summary>
        [EnumMember(Value = "portrait")]
        Portrait,

        /// <summary>
        /// Landscape
        /// </summary>
        [EnumMember(Value = "landscape")]
        Landscape
    }
}
