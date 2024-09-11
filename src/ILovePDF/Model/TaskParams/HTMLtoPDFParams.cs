using iLovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    ///  Html to PDF Params
    /// </summary>
    public class HTMLtoPDFParams : BaseParams
    {
        private int delay;
        private int navigationTimeout;

        /// <summary>
        /// Html to PDF Params Constructor
        /// </summary>
        public HTMLtoPDFParams()
        {
            SetDefaultValues();
        } 

        /// <summary> 
        /// Viewer width
        /// </summary> 
        [JsonProperty("view_width")]
        public int ViewWidth { get; set; }

        /// <summary> 
        /// Viewer height
        /// </summary> 
        [JsonProperty("view_height")]
        public int? ViewHeight { get; set; }
         
        /// <summary> 
        /// Time to waith for page response 
        /// - value  must be between 0 and 20
        /// - default is 10
        /// </summary> 
        [JsonProperty("navigation_timeout")]
        public int NavigationTimeout 
        { 
            get => navigationTimeout;
            set
            {
                if (value < 0 || value > 20) {
                    throw new ArgumentOutOfRangeException(nameof(NavigationTimeout), "Navigation timeout must be under 20 seconds");
                }
                navigationTimeout = value;
            }
        }

        /// <summary> 
        /// Time to wait for javscript execution in page
        /// - value  must be between 0 and 5
        /// - default is 2
        /// </summary> 
        [JsonProperty("delay")]
        public int Delay
        {
            get => delay;
            set
            {
                if (value < 0 || value > 5)
                {
                   throw new ArgumentOutOfRangeException(nameof(Delay), "Delay must be under 5 seconds");
                }
                delay = value;
            }
        }

        /// <summary> 
        /// Final PDF document size.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("page_size")]
        public DocumentPageSizes? PageSize { get; set; }

        /// <summary> 
        /// Final PDF file orientation: portrait or landscape.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("page_orientation")]
        public Orientations? Orientation { get; set; }

        /// <summary> 
        /// Pixels for page margin.
        /// </summary> 
        [JsonProperty("page_margin")]
        public int Margin { get; set; }

        /// <summary> 
        /// Remove z-index (high value) based elements.
        /// </summary> 
        [JsonProperty("remove_popups")]
        public bool RemovePopups { get; set; }


        /// <summary> 
        /// Make a single output page.
        /// </summary> 
        [JsonProperty("single_page")]
        public bool SinglePage { get; set; }

        //
        private void SetDefaultValues()
        {
            ViewWidth = 1920;
            ViewHeight = null;
            NavigationTimeout = 10;
            Delay = 2;
            PageSize = null;
            Orientation = null;
            Margin = 0;
            RemovePopups = false;
            SinglePage = false;
        }
    }
}