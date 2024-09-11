using System;
using System.Runtime.Serialization;

namespace iLovePdf.Model.Exception
{
    /// <summary>
    ///     Processing Failed Exception.
    /// </summary>
    /// <remarks>
    /// </remarks>
    [Serializable]
    public class ProcessingException : System.Exception
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public ProcessingException()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public ProcessingException(String message) : base(message)
        {
        }

        /// <summary>
        ///     Init a new Instance of the class ILovePDF.ProcessingException
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner Exception.</param>
        public ProcessingException(String message, System.Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_5
        /// <summary>
        ///     Init a new Instance of the class ILovePDF.ProcessingException
        /// </summary>
        /// <param name="info"> Serialization Information.</param>
        /// <param name="context"> Streaming Context.</param>
        protected ProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}