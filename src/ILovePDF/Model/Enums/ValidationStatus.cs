using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    ///     Validation Status
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        ///     Conformant
        /// </summary>
        [EnumMember(Value = "Conformant")] Conformant,

        /// <summary>
        ///     NonConformant
        /// </summary>
        [EnumMember(Value = "NonConformant")] NonConformant
    }
}