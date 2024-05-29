using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Sign Element Types
    /// </summary>
    public enum SignElementTypes
    {
        /// <summary>
        /// Signer initials.
        /// </summary>
        [EnumMember(Value = "initials")] Initials,

        /// <summary>
        /// Signer signature.
        /// </summary>
        [EnumMember(Value = "signature")] Signature,

        /// <summary>
        /// Signer full name.
        /// </summary>
        [EnumMember(Value = "name")] Name,

        /// <summary>
        /// Date when the signature is applied.
        /// </summary>
        [EnumMember(Value = "date")] Date,

        /// <summary>
        /// Text to stamp above the pdf content.
        /// </summary>
        [EnumMember(Value = "text")] Text,

        /// <summary>
        /// The signer must fill the input.
        /// </summary>
        [EnumMember(Value = "input")] Input,
    }
}