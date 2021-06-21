using System;
using Newtonsoft.Json;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class EditPdfParamsElementBase
    {
        /// <summary>
        ///     Type of the element to be added
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        public abstract String Type { get; }

        /// <summary>
        ///     The page where the element will be inserted. Only numbers format is allowed.
        /// </summary>
        [JsonProperty("pages", Required = Required.Always)]
        public String Pages { get; set; } = "all";

        /// <summary>
        ///     Depth value to decide elements order in case of overlay.
        /// </summary>
        [JsonProperty("zindex", Required = Required.Always)]
        public Int32 ZIndex { get; set; }

        /// <summary>
        ///     Size of the element following the format { x: 120, y: 200 }.
        /// </summary>
        [JsonProperty("dimensions", Required = Required.Always)]
        public PdfCoordinates Dimensions { get; set; }

        /// <summary>
        ///     Position of the element in X and Y coordinates following the format { x: 120, y: 200 }.
        /// </summary>
        [JsonProperty("coordinates", Required = Required.Always)]
        public PdfCoordinates Coordinates  { get; set; }

        /// <summary>
        ///     Angle of rotation. Accepted integer range: 0-360.
        /// </summary>
        [JsonProperty("rotation")]
        public Int32? Rotation { get; set; }

        /// <summary>
        ///     Percentage of opacity for stamping element. Accepted integer range 1-100.
        /// </summary>
        [JsonProperty("opacity")]
        public Int32? Opacity { get; set; }
    }
}