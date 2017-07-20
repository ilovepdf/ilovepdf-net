using System;
using System.Runtime.Serialization;

namespace LovePdf.Model.Exception
{
    /// <summary>
    /// Server Error Exeption
    /// </summary>
    [Serializable]
    public class ServerErrorException : System.Exception
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ServerErrorException() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ServerErrorException(string message) : base(message)
        {
        }

        /// <summary>
        /// Init a new Instance of the class ILovePDF.ServerErrorException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ServerErrorException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Init a new Instance of the class ILovePDF.ServerErrorException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}