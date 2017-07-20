using System;
using System.Runtime.Serialization;

namespace LovePdf.Model.Exception
{
    /// <summary>
    /// Too Many Requests Exception
    /// </summary>
    [Serializable]
    public class TooManyRequestsException : System.Exception
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TooManyRequestsException() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TooManyRequestsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Init a new Instance of the class ILovePDF.TooManyRequestsException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TooManyRequestsException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Init a new Instance of the class ILovePDF.TooManyRequestsException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TooManyRequestsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}