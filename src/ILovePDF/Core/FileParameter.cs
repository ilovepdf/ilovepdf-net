using System;
using System.IO;

namespace iLovePdf.Core
{
    /// <summary>
    ///     File Parameter
    /// </summary>
    internal sealed class FileParameter
    {
        /// <summary>
        ///     File Parameter Constructor
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        public FileParameter(Stream file, String fileName)
        {
            FileStream = file;
            FileName = fileName;
        }

        /// <summary>
        ///     File Parameter Constructor
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        public FileParameter(Byte[] file, String fileName)
        {
            File = file;
            FileName = fileName;
        }

        /// <summary>
        ///     File byte[]
        /// </summary>
        public Byte[] File { get; set; }

        /// <summary>
        ///     File Name
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        ///     File Stream
        /// </summary>
        public Stream FileStream { get; set; }
    }
}