using System;
using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditPdfParamsElementText : EditPdfParamsElementBase
    {
        /// <inheritdoc />
        public override String Type => "text";
        
        /// <summary>
        ///     Text to add to WaterMark
        /// </summary>
        [JsonProperty("text", Required = Required.Always)]
        public String Text { get; set; }

        /// <summary>
        ///     Horizontal text alignment. Accepted values: left, center, right.
        /// </summary>
        [JsonProperty("text_align")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WaterMarkHorizontalPositions? TextAlign { get; set; }

        /// <summary>
        ///     Font type of text. See Fonts compatibility for complete list.
        /// </summary>
        [JsonProperty("font_family")]
        public String FontFamily { get; set; }

        /// <summary>
        ///     Size of text font in pixels.
        /// </summary>
        [JsonProperty("font_size")]
        public Int32 FontSize { get; set; }

        /// <summary>
        ///     Text font styles. Accepted values: Regular, Bold, Italic, Bold italic
        /// </summary>
        [JsonProperty("font_style")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FontStyles? FontStyle { get; set; }

        /// <summary>
        ///     Text font color. Can be set to transparent. Otherwise, the accepted value is hexadecimal.
        /// </summary>
        [JsonProperty("font_color", NullValueHandling = NullValueHandling.Ignore)]
        public String FontColor { get; set; }

        /// <summary>
        ///     Text letter spacing between characters. Accepted values from 0 to infinite.
        /// </summary>
        [JsonProperty("letter_spacing")]
        public Int32? LetterSpacing  { get; set; }

        /// <summary>
        ///     Text underline active or inactive. Values can be true or false.
        /// </summary>
        [JsonProperty("underline_text")]
        public Boolean? UnderlineText  { get; set; }
    }
}