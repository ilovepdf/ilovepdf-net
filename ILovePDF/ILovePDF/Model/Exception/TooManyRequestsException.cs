using System;
using System.Runtime.Serialization;

namespace LovePdf.Model.Exception
{
    /// <summary>
    ///     Too Many Requests Exception
    /// </summary>
    [Serializable]
    public class TooManyRequestsException : System.Exception
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public TooManyRequestsException()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public TooManyRequestsException(String message) : base(message)
        {
        }

        /// <summary>
        ///     Init a new Instance of the class ILovePDF.TooManyRequestsException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TooManyRequestsException(String message, System.Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_5
        /// <summary>
        ///     Init a new Instance of the class ILovePDF.TooManyRequestsException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TooManyRequestsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}