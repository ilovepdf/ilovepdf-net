using System;
using System.Runtime.Serialization;

namespace iLovePdf.Model.Exception
{
    /// <summary>
    ///     Upload Failed Exception.
    /// </summary>
    /// <remarks>
    /// </remarks>
    [Serializable]
    public class UploadException : System.Exception
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public UploadException()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public UploadException(String message) : base(message)
        {
        }

        /// <summary>
        ///     Init a new Instance of the class ILovePDF.UploadException
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner Exception.</param>
        public UploadException(String message, System.Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_5
        /// <summary>
        ///     Init a new Instance of the class ILovePDF.UploadException
        /// </summary>
        /// <param name="info"> Serialization Information.</param>
        /// <param name="context"> Streaming Context.</param>
        protected UploadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}