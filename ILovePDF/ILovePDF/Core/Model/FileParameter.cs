using System.IO;

namespace ILovePDF.Core.Model
{
    public class FileParameter
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
        public string ContentType { get; set; }
        public FileParameter(byte[] file) : this(file, null) { }
        public FileParameter(byte[] file, string filename) : this(file, filename, null) { }

        public FileParameter(Stream file, string filename)
        {
            FileStream = file;
            FileName = filename;
        }
        public FileParameter(byte[] file, string filename, string contenttype)
        {
            File = file;
            FileName = filename;
            ContentType = contenttype;
        }
    }
}
