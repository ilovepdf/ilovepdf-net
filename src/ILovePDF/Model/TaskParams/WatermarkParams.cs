using System;
using System.Collections.Generic;
using iLovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    ///     WaterMarkParams
    /// </summary>
    public class WaterMarkParams : BaseParams
    {
        /// <summary>
        ///     Params
        /// </summary>
        /// <param name="mode"></param>
        public WaterMarkParams(WatermarkModeText mode)
        {
            if (mode == null)
                throw new ArgumentException("cannot be null", nameof(mode));

            setDefaultValues();
            Mode = WaterMarkModes.Text;
            Text = mode.Text;
        }

        /// <summary>
        ///     Params
        /// </summary>
        /// <param name="mode"></param>
        public WaterMarkParams(WatermarkModeImage mode)
        {
            if (mode == null)
                throw new ArgumentException("cannot be null", nameof(mode));

            setDefaultValues();
            Mode = WaterMarkModes.Image;
            Image = mode.ServerFileName;
        }

        /// <summary>
        ///     Params
        /// </summary>
        /// <param name="elements"></param>
        public WaterMarkParams(IEnumerable<WaterMarkParamsElement> elements)
        {
            if (elements == null)
                throw new ArgumentException("cannot be null", nameof(elements));

            setDefaultValues();
            Mode = WaterMarkModes.Multi;
            Elements.AddRange(elements);

            if (Elements.Count == 0)
                throw new ArgumentException("cannot be empty", nameof(elements));
        }

        /// <summary>
        ///     Mode (text || image)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("mode")]
        public WaterMarkModes Mode { get; set; }

        /// <summary>
        ///     Text to add to WaterMark
        /// </summary>
        [JsonProperty("text")]
        public String Text { get; set; }

        /// <summary>
        ///     Image to add to WaterMark
        /// </summary>
        [JsonProperty("image")]
        public String Image { get; set; }

        /// <summary>
        ///     Pages to add the WaterMark
        /// </summary>
        [JsonProperty("pages")]
        public String Pages { get; set; }

        /// <summary>
        ///     Vertical Position of the WaterMark
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("vertical_position")]
        public WaterMarkVerticalPositions? VerticalPosition { get; set; }

        /// <summary>
        ///     Horizontal Position of the WaterMark
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("horizontal_position")]
        public WaterMarkHorizontalPositions? HorizontalPosition { get; set; }

        /// <summary>
        ///     Fine Adjustment of the Vertical Position
        /// </summary>
        [JsonProperty("vertical_position_adjustment")]
        public Int32 VerticalPositionAdjustment { get; set; }

        /// <summary>
        ///     Fine Adjustment of the horizontal position
        /// </summary>
        [JsonProperty("horizontal_position_adjustment")]
        public Int32 HorizontalPositionAdjustment { get; set; }

        /// <summary>
        ///     Mosaic effect on/off
        /// </summary>
        [JsonProperty("mosaic")]
        public Boolean Mosaic { get; set; }

        /// <summary>
        ///     Rotation of the WaterMark
        /// </summary>
        [JsonProperty("rotation")]
        public Int32 Rotation { get; set; }

        /// <summary>
        ///     Font Family of the WaterMark
        /// </summary>
        [JsonProperty("font_family")]
        public String FontFamily { get; set; }

        /// <summary>
        ///     Font Style of the WaterMark
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("font_style")]
        public FontStyles? FontStyle { get; set; }

        /// <summary>
        ///     Font Size of the WaterMark
        /// </summary>
        [JsonProperty("font_size")]
        public Int32 FontSize { get; set; }

        /// <summary>
        ///     Font Color of the WaterMark
        /// </summary>
        [JsonProperty("font_color")]
        public String FontColor { get; set; }

        /// <summary>
        ///     Transparency of the WaterMark
        /// </summary>
        [JsonProperty("transparency")]
        public Int32 Transparency { get; set; }

        /// <summary>
        ///     Position of the WaterMark above or below the original Pdf
        /// </summary>
        [JsonProperty("layer")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Layer? Layer { get; set; }

        /// <summary>
        ///     Position of the WaterMark above or below the original Pdf
        /// </summary>
        [JsonIgnore]
        public List<WaterMarkParamsElement> Elements { get; private set; }

        private void setDefaultValues()
        {
            Mode = WaterMarkModes.Text;
            Text = null;
            Image = null;
            Pages = "all";
            VerticalPosition = null;
            HorizontalPosition = null;
            VerticalPositionAdjustment = 0;
            HorizontalPositionAdjustment = 0;
            Mosaic = false;
            Rotation = 0;
            FontFamily = "Arial Unicode MS";
            FontStyle = null;
            FontSize = 14;
            FontColor = "#000000";
            Transparency = 100;
            Layer = null;
            Elements = new List<WaterMarkParamsElement>();
        }
    }
}