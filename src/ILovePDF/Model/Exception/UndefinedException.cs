using System;
using System.Runtime.Serialization;

namespace iLovePdf.Model.Exception
{
    /// <summary>
    ///     Sign Start Exeption
    /// </summary>
    [Serializable]
    public class UndefinedException : System.Exception
    {
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public UndefinedException()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public UndefinedException(String message) : base(message)
        {
        }

        /// <summary>
        ///     Init a new Instance of the class ILovePDF.UndefinedException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UndefinedException(String message, System.Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_5
        /// <summary>
        ///     Init a new Instance of the class ILovePDF.UndefinedException
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected UndefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}