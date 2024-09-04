using System;
using System.Runtime.Serialization;

namespace iLovePdf.Model.Exception
{
    /// <summary>
    ///     Sign Start Exeption
    /// </summary>
    [Serializable]
    public class SignStartException : System.Exception
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public SignStartException()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public SignStartException(String message) : base(message)
        {
        }

        /// <summary>
        ///     Init a new Instance of the class ILovePDF.SignStartException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SignStartException(String message, System.Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_5
        /// <summary>
        ///     Init a new Instance of the class ILovePDF.SignStartException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SignStartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}