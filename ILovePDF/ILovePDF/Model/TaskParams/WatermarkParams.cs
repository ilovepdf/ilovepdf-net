using System;
using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// WaterMarkParams
    /// </summary>
    public class WaterMarkParams : BaseParams
    {
        /// <summary>
        /// Mode (text || image)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("mode")]
        public WaterMarkModes Mode { get; set; }

        /// <summary>
        /// Text to add to WaterMark
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Image to add to WaterMark
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        /// Pages to add the WaterMark
        /// </summary>
        [JsonProperty("pages")]
        public string Pages { get; set; }

        /// <summary>
        /// Vertical Position of the WaterMark
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("vertical_position")]
        public WaterMarkVerticalPositions? VerticalPosition { get; set; }

        /// <summary>
        /// Horizontal Position of the WaterMark
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("horizontal_position")]
        public WaterMarkHorizontalPositions? HorizontalPosition { get; set; }

        /// <summary>
        /// Fine Adjustment of the Vertical Position
        /// </summary>
        [JsonProperty("vertical_position_adjustment")]
        public int VerticalPositionAdjustment { get; set; }

        /// <summary>
        /// Fine Adjustment of the horizontal position
        /// </summary>
        [JsonProperty("horizontal_position_adjustment")]
        public int HorizontalPositionAdjustment { get; set; }

        /// <summary>
        /// Mosaic effect on/off
        /// </summary>
        [JsonProperty("mosaic")]
        public bool Mosaic { get; set; }

        /// <summary>
        /// Rotation of the WaterMark
        /// </summary>
        [JsonProperty("rotation")]
        public int Rotation { get; set; }

        /// <summary>
        /// Font Family of the WaterMark
        /// </summary>
        [JsonProperty("font_family")]
        public string FontFamily { get; set; }

        /// <summary>
        /// Font Style of the WaterMark
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("font_style")]
        public FontStyles? FontStyle { get; set; }

        /// <summary>
        /// Font Size of the WaterMark
        /// </summary>
        [JsonProperty("font_size")]
        public int FontSize { get; set; }

        /// <summary>
        /// Font Color of the WaterMark
        /// </summary>
        [JsonProperty("font_color")]
        public string FontColor { get; set; }

        /// <summary>
        /// Transparency of the WaterMark
        /// </summary>
        [JsonProperty("transparency")]
        public int Transparency { get; set; }

        /// <summary>
        /// Position of the WaterMark above or below the original Pdf
        /// </summary>
        [JsonProperty("layer")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Layer? Layer { get; set; }

        /// <summary>
        /// Params
        /// </summary>
        /// <param name="mode"></param>
        public WaterMarkParams(WatermarkModeText mode)
        {
            if (mode == null)
                throw new ArgumentException("cannot be null", nameof(mode));

            SetDefaultValues();
            Mode = WaterMarkModes.Text;
            Text = mode.Text;

        }

        /// <summary>
        /// Params
        /// </summary>
        /// <param name="mode"></param>
        public WaterMarkParams(WatermarkModeImage mode)
        {
            if (mode == null)
                throw new ArgumentException("cannot be null", nameof(mode));

            SetDefaultValues();
            Mode = WaterMarkModes.Image;
            Image = mode.ServerFileName;
        }

        private void SetDefaultValues()
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
        }
    }
}