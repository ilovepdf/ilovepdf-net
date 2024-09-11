using System;
using System.Runtime.Serialization;

namespace iLovePdf.Model.Exception
{
    /// <summary>
    ///     Download Failed Exception.
    /// </summary>
    /// <remarks>
    /// </remarks>
    [Serializable]
    public class DownloadException : System.Exception
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public DownloadException()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public DownloadException(String message) : base(message)
        {
        }

        /// <summary>
        ///     Init a new Instance of the class ILovePDF.DownloadException
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner Exception.</param>
        public DownloadException(String message, System.Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_5
        /// <summary>
        ///     Init a new Instance of the class ILovePDF.DownloadException
        /// </summary>
        /// <param name="info"> Serialization Information.</param>
        /// <param name="context"> Streaming Context.</param>
        protected DownloadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}