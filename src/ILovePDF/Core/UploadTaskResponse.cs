using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

                int number = Convert.ToInt32(x);

                var pdfpageinfo = GetPdfPageInfo(number);
                foreach (var page in pdfpageinfo) 
                {
                    pdfFormElement.Add(page.Key, page.Value);
                }                
            }
        }

        public Dictionary<string, double> GetPdfPageInfo(int pageNumber)
        {
            var pdfPages = GetSanitizedPdfPages();
            if (pdfPages == null)
            {
                return null;
            }

            return pdfPages[pageNumber - 1];
        }

        public List<Dictionary<string, double>> GetSanitizedPdfPages()
        {
            var result = new List<Dictionary<string, double>>();

            if (PdfPages == null)
            {
                return null;
            }


            foreach(var pdfPage in PdfPages)
            {
                var dimensions = pdfPage.Split('x');
                double width = Convert.ToDouble(dimensions[0], CultureInfo.InvariantCulture);
                double height = Convert.ToDouble(dimensions[1], CultureInfo.InvariantCulture);

                result.Add(new Dictionary<string, double>
                {
                    { "width", width },
                    { "height", height }
                });
            }

            return result;
        }
    }
}