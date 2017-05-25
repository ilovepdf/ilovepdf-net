using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection;

namespace ILovePDF.Model.TaskParams
{
    public abstract class BaseParams
    {
        [JsonProperty("ignore_errors")]
        public bool IgnoreErrors { get; set; }

        [JsonProperty("ignore_password")]
        public bool IgnorePassword { get; set; }

        /// <summary>
        /// {date}=current date, {n}=file number, {filename}=original filename, {tool}=the current processing action. Example: file_{n}_{date}
        /// </summary>
        [JsonProperty("output_filename")]
        public string OutputFileName { get; set; }
        /// <summary>
        /// If output files are more than one will be served compressed. Specify the filename of the compressed file. {date}=current date, {n}=file number,{filename}=original filename, {app}=the current processing action. Example: zipped_{n}_{date} 
        /// Default filname: output.zip
        /// </summary>
        [JsonProperty("package_filename")]
        public string PackageFileName { get; set; }

        /// <summary>
        /// If specified it is assumed all previously uploaded files for the task has been uploaded encrypted. The key will be used to decrypt the files before processing and re-encrypt them after processing. Only keys of sizes 16, 24 or 32 are supported.
        /// Default: null
        /// </summary>
        [JsonProperty("file_encryption_key")]
        public string FileEncryptionKey { get; set; }

        /// <summary>
        /// When a PDF to process fails we try to repair it automatically. 
        /// </summary>
        [JsonProperty("try_pdf_repair")]
        public bool TryPDFRepair { get; set; }

        protected BaseParams()
        {
            SetDefaultProps();
        }

        private void SetDefaultProps()
        {
            TryPDFRepair = true;
            IgnoreErrors = true;
        }

        protected string GetEnumDescription(System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }

    public class OfficePdf : BaseParams{ }

    public class RotateParams : BaseParams { }

    public class RepairParams : BaseParams { }

    public class UnlockParams : BaseParams { }
}
