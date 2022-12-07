using System;
using System.Runtime.Serialization;

namespace LovePdf.Model.Exception
{
    /// <summary>
    ///     Sign Start Exeption
    /// </summary>
    [Serializable]
    public class SignatureException : System.Exception
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public SignatureException()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public SignatureException(String message) : base(message)
        {
        }

        /// <summary>
        ///     Init a new Instance of the class ILovePDF.SignStartException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SignatureException(String message, System.Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_5
        /// <summary>
        ///     Init a new Instance of the class ILovePDF.SignStartException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}