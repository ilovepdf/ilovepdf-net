﻿using System;
using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     Page Number Params
    /// </summary>
    public class PageNumbersParams : BaseParams
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public PageNumbersParams()
        {
            setDefaultValues();
        }

        /// <summary>
        ///     Facing Pages
        /// </summary>
        [JsonProperty("facing_pages")]
        public Boolean FacingPages { get; set; }

        /// <summary>
        ///     First Cover
        /// </summary>
        [JsonProperty("first_cover")]
        public Boolean FirstCover { get; set; }

        /// <summary>
        ///     Pages
        /// </summary>
        [JsonProperty("pages")]
        public String Pages { get; set; }

        /// <summary>
        ///     Starting Number
        /// </summary>
        [JsonProperty("starting_number")]
        public Int32 StartingNumber { get; set; }

        /// <summary>
        ///     Vertical Position
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("vertical_position")]
        public VerticalPositions VerticalPosition { get; set; }

        /// <summary>
        ///     Horizontal Position
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("horizontal_position")]
        public HorizontalPositions HorizontalPosition { get; set; }

        /// <summary>
        ///     Fine Adjustment of vertical position
        /// </summary>
        [JsonProperty("vertical_position_adjustment")]
        public Int32 VerticalPositionAdjustment { get; set; }

        /// <summary>
        ///     Fine Adjustment of horizontal position
        /// </summary>
        [JsonProperty("horizontal_position_adjustment")]
        public Int32 HorizontalPositionAdjustment { get; set; }

        /// <summary>
        ///     Font Family
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("font_family")]
        public FontFamilies FontFamily { get; set; }

        /// <summary>
        ///     Font Style
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("font_style")]
        public FontStyles? FontStyle { get; set; }

        /// <summary>
        ///     Font Size
        /// </summary>
        [JsonProperty("font_size")]
        public Int32 FontSize { get; set; }

        /// <summary>
        ///     Font Color
        /// </summary>
        [JsonProperty("font_color")]
        public String FontColor { get; set; }

        /// <summary>
        ///     Text
        /// </summary>
        [JsonProperty("text")]
        public String Text { get; set; }

        private void setDefaultValues()
        {
            FacingPages = false;
            FirstCover = false;
            Pages = @"all";
            StartingNumber = 1;
            VerticalPosition = VerticalPositions.Bottom;
            HorizontalPosition = HorizontalPositions.Middle;
            VerticalPositionAdjustment = 0;
            HorizontalPositionAdjustment = 0;
            FontFamily = FontFamilies.ArialUnicodeMs;
            FontStyle = null;
            FontColor = "#000000";
            Text = @"{n}";
        }
    }
}