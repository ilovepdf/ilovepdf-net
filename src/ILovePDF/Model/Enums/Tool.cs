using System.ComponentModel;

// EnumExtensions.GetEnumDescription(LovePdfErrors.UploadError)

namespace LovePdf.Model.Enums
{
    /// <summary>
    ///     Available tasks
    /// </summary>
    public enum TaskName
    {
        /// <summary>
        ///     Merge PDFs
        /// </summary>
        [Description("merge")] Merge = 0,

        /// <summary>
        ///     Split PDFs
        /// </summary>
        [Description("split")] Split = 1,

        /// <summary>
        ///     Compress PDFs
        /// </summary>
        [Description("compress")] Compress = 2,

        /// <summary>
        ///     Convert PDFs to Office
        /// </summary>
        [Description("officepdf")] OfficeToPdf = 3,

        /// <summary>
        ///     Convert PDFs to JPG
        /// </summary>
        [Description("pdfjpg")] PdfToJpg = 4,

        /// <summary>
        ///     Extract images from PDFs
        /// </summary>
        [Description("imagepdf")] ImagePdf = 5,

        /// <summary>
        ///     Add watermarks to PDFs
        /// </summary>
        [Description("watermark")] WaterMark = 6,

        /// <summary>
        ///     Add page numbers to PDFs
        /// </summary>
        [Description("pagenumber")] PageNumber = 7,

        /// <summary>
        ///     Unlock PDFs
        /// </summary>
        [Description("unlock")] Unlock = 8,

        /// <summary>
        ///     Rotate PDFs
        /// </summary>
        [Description("rotate")] Rotate = 9,

        /// <summary>
        ///     Try to repar damaged PDFs
        /// </summary>
        [Description("repair")] Repair = 10,

        /// <summary>
        ///     Protect PDFs
        /// </summary>
        [Description("protect")] Protect = 11,

        /// <summary>
        ///     Validate PDFA compliance
        /// </summary>
        [Description("validatepdfa")] ValidatePdfA = 12,

        /// <summary>
        ///     PDFa
        /// </summary>
        [Description("pdfa")] PdfToPdfA = 13,

        /// <summary>
        ///     Extract
        /// </summary>
        [Description("extract")] Extract = 14,

        /// <summary>
        /// Html To Pdf
        /// </summary>
        [Description("htmlpdf")]
        HtmlToPdf = 15,

        /// <summary>
        /// Edit
        /// </summary>
        [Description("editpdf")] Edit = 16,

        /// <summary>
        /// Edit
        /// </summary>
        [Description("sign")] Sign = 17,

        /// <summary>
        /// Edit
        /// </summary>
        [Description("pdfocr")] Pdfocr = 18,
    }
}