using System;
using System.Runtime.Serialization;

namespace LovePdf.Model.Exception
{
    /// <summary>
    ///     Server Error Exeption
    /// </summary>
    [Serializable]
    public class ServerErrorException : System.Exception
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public ServerErrorException()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public ServerErrorException(String message) : base(message)
        {
        }

        /// <summary>
        ///     Init a new Instance of the class ILovePDF.ServerErrorException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ServerErrorException(String message, System.Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_5
        /// <summary>
        ///     Init a new Instance of the class ILovePDF.ServerErrorException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}