using System.IO;

namespace LovePdf.Core
{
    /// <summary>
    /// File Parameter
    /// </summary>
    internal class FileParameter
    {
        /// <summary>
        /// File byte[]
        /// </summary>
        public byte[] File { get; set; }
        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// File Stream
        /// </summary>
        public Stream FileStream { get; set; }

        /// <summary>
        /// File Parameter Constructor
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        public FileParameter(Stream file, string fileName)
        {
            FileStream = file;
            FileName = fileName;
        }
        /// <summary>
        /// File Parameter Constructor
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        public FileParameter(byte[] file, string fileName)
        {
            File = file;
            FileName = fileName;
        }
    }
}
