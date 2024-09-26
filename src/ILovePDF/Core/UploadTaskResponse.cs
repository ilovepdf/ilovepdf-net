using System;
using Newtonsoft.Json;

namespace iLovePdf.Core
{
    /// <summary>
    ///     Upload Task Response
    /// </summary>
    public class UploadTaskResponse
    {
        /// <summary>
        ///     Server file name
        /// </summary>
        [JsonProperty("server_filename")]
        public String ServerFileName { get; set; }
        /// <summary>
        ///     Server file name
        /// </summary>
        [JsonProperty("pdf_pages")]
        public String[] PdfPages { get; set; }
        /// <summary>
        ///     Server file name
        /// </summary>
        [JsonProperty("pdf_page_number")]
        public String PdfPageNumber { get; set; }
    }
}