using System;
using System.Runtime.Serialization;

namespace LovePdf.Model.Exception
{
    /// <summary>
    /// Not Found Excepcion
    /// </summary>
    [Serializable]
    public class NotFoundException : System.Exception
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public NotFoundException() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public NotFoundException(string message) : base(message)
        {
        }
        
        /// <summary>
        /// Init a new Instance of the class ILovePDF.NotFoundException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Init a new Instance of the class ILovePDF.NotFoundException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}