using System;
using LovePdf.Model.Enums;
using Newtonsoft.Json;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     HTML to PDF Params
    /// </summary>
    public class HtmlToPdfParams : BaseParams
    {
        /// <summary>
        ///     Viewer width
        /// </summary>
        [JsonProperty("view_width")]
        public Int32 ViewWidth { get; set; } = 1980;

        /// <summary>
        ///     Create single-page document
        /// </summary>
        [JsonProperty("single-page", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean? NavigationTimeout { get; set; }

        /// <summary>
        ///     Page margin
        /// </summary>
        [JsonProperty("page-margin", NullValueHandling = NullValueHandling.Ignore)]
        public Int32? PageMargin { get; set; }

        /// <summary>
        ///     Page size
        /// </summary>
        [JsonProperty("page_size", NullValueHandling = NullValueHandling.Ignore)]
        public PageSizes? PageSize { get; set; }

        /// <summary>
        ///     Page orientation
        /// </summary>
        [JsonProperty("page_orientation", NullValueHandling = NullValueHandling.Ignore)]
        public Orientations? PageOrientation { get; set; }
    }
}