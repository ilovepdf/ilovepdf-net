using System.ComponentModel;

namespace LovePdf.Model.Enums
{
    /// <summary>
    ///     iLovelPdf errors
    /// </summary>
    public enum LovePdfErrors
    {
        /// <summary>
        ///     Upload error
        /// </summary>
        [Description("UploadError")] UploadError,

        /// <summary>
        ///     Processing error
        /// </summary>
        [Description("ProcessingError")] ProcessingError,

        /// <summary>
        ///     Download error
        /// </summary>
        [Description("DownloadError")] DownloadError
    }
}