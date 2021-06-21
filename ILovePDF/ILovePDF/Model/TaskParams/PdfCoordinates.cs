using System;
using Newtonsoft.Json;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PdfCoordinates
    {
        /// <summary>
        ///     The X coordinate/size of the element on PDF page.
        /// </summary>
        [JsonProperty("x", Required = Required.Always)]
        public Double X { get; set; }

        /// <summary>
        ///     The Y coordinate/size of the element on PDF page.
        /// </summary>
        [JsonProperty("y", Required = Required.Always)]
        public Double Y { get; set; }

    }
}