using System.Runtime.Serialization;

namespace iLovePdf.Model.Enums
{
    /// <summary>
    /// Force Signature Type
    /// </summary>
    public enum ForceSignatureType
    {
        /// <summary>
        /// The signer can choose the preferred signature adoption.
        /// </summary>
        [EnumMember(Value = "all")] All,

        /// <summary>
        /// The signer must sign with a pre-defined font.
        /// </summary>
        [EnumMember(Value = "text")] Text,

        /// <summary>
        /// The signer must sign drawing a signature.
        /// </summary>
        [EnumMember(Value = "sign")] Sign,

        /// <summary>
        /// The signer must sign drawing a signature.
        /// </summary>
        [EnumMember(Value = "image")] Image,
    }
}