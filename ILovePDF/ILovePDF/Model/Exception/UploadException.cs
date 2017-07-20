using System;
using System.Runtime.Serialization;

namespace LovePdf.Model.Exception
{
    /// <summary>
    /// Upload Failed Exception.
    /// </summary>
    /// <remarks>
    /// </remarks>
    [Serializable]
    public class UploadException : System.Exception
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public UploadException() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UploadException(string message) : base(message)
        {
        }

        /// <summary>
        /// Init a new Instance of the class ILovePDF.UploadException
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner Exception.</param>
        public UploadException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Init a new Instance of the class ILovePDF.UploadException
        /// </summary>
        /// <param name="info"> Serialization Information.</param>
        /// <param name="context"> Streaming Context.</param>
        protected UploadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
