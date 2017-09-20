using Newtonsoft.Json;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// Base Params
    /// </summary>
    public abstract class BaseParams
    {
        /// <summary>
        /// Ignore Errors
        /// </summary>
        [JsonProperty("ignore_errors")]
        public bool IgnoreErrors { get; set; }

        /// <summary>
        /// Ignore Password
        /// </summary>
        [JsonProperty("ignore_password")]
        public bool IgnorePassword { get; set; }

        /// <summary>
        /// {date}=current date, {n}=file number, {filename}=original filename, {tool}=the current processing action. Example: file_{n}_{date}
        /// </summary>
        [JsonProperty("output_filename")]
        public string OutputFileName { get; set; }
        /// <summary>
        /// If output files are more than one will be served compressed. Specify the filename of the compressed file. {date}=current date, {n}=file number,{filename}=original filename, {app}=the current processing action. Example: zipped_{n}_{date} 
        /// Default filename: output.zip
        /// </summary>
        [JsonProperty("packaged_filename")]
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
        public bool TryPdfRepair { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseParams()
        {
            SetDefaultProps();
        }

        private void SetDefaultProps()
        {
            TryPdfRepair = true;
            IgnoreErrors = true;
        }

    }

    /// <summary>
    /// Office To Pdf Params
    /// </summary>
    public class OfficeToPdfParams : BaseParams { }

    /// <summary>
    /// Rotate Params
    /// </summary>
    public class RotateParams : BaseParams { }

    /// <summary>
    /// Repair Params
    /// </summary>
    public class RepairParams : BaseParams { }

    /// <summary>
    /// Unlock Params
    /// </summary>
    public class UnlockParams : BaseParams { }
}
