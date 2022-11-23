using System.Runtime.Serialization;

namespace LovePdf.Model.Enums
{
    /// <summary>
    /// Sign Element Types
    /// </summary>
    public enum SignSignerType
    {
        /// <summary>
        /// The signer is a receiver type that can reject or sign a document.
        /// </summary>
        [EnumMember(Value = "signer")] Signer,

        /// <summary>
        /// The validator is a receiver type that has ability to 
        /// validate a document. This receiver type is commonly 
        /// used to either validate the content of a document or 
        /// the signing process once it is completed. The validator
        /// can validate or reject a document.
        /// </summary>
        [EnumMember(Value = "validator")] Validator,

        /// <summary>
        /// The viewer (also called witness in the rest of the 
        /// document) is a receiver type that receives a copy of
        /// the original document and the completely signed one 
        /// as well. Use this type if someone must only receive 
        /// and view the document but they don't need to take any
        /// action on it.
        /// </summary>
        [EnumMember(Value = "viewer")] Viewer, 
    }
}