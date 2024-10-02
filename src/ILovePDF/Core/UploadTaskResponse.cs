using System;
using System.Collections.Generic;
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
        ///     pdf_pages
        /// </summary>
        [JsonProperty("pdf_pages")]
        public String[] PdfPages { get; set; }
        /// <summary>
        ///     pdf_page_number
        /// </summary>
        [JsonProperty("pdf_page_number")]
        public String PdfPageNumber { get; set; }
        /// <summary>
        ///     pdf_forms
        /// </summary>
        [JsonProperty("pdf_forms")]
        public List<Dictionary<string, object>> PdfForms { get; set; }


        public void GetPdfFormElement()
        {

            object x;
            var pdfPageInfo = new Dictionary<string, int>();

            if (PdfForms == null || PdfForms.Count == 0)
            {
                return;
            }

            foreach (var pdfFormElement in PdfForms)
            {
                pdfFormElement.TryGetValue("page", out x);
                var pdfpageinfo = GetPdfPageInfo((int)x);
                foreach (var page in pdfpageinfo) 
                {
                    pdfPageInfo.Add(page.Key, page.Value);
                }                
            }
        }

        public Dictionary<string, int> GetPdfPageInfo(int pageNumber)
        {
            var pdfPages = GetSanitizedPdfPages();
            if (pdfPages == null)
            {
                return null;
            }

            return pdfPages[pageNumber - 1];
        }

        public List<Dictionary<string, int>> GetSanitizedPdfPages()
        {
            var result = new List<Dictionary<string, int>>();

            if (PdfPages == null)
            {
                return null;
            }


            foreach(var pdfPage in PdfPages)
            {
                var dimensions = pdfPage.Split('x');
                int width = int.Parse(dimensions[0]);
                int height = int.Parse(dimensions[1]);

                result.Add(new Dictionary<string, int>
                {
                    { "width", width },
                    { "height", height }
                });
            }

            return result;
        }
    }
}