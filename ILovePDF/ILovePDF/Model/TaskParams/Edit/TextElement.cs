using LovePdf.Attributes;
using LovePdf.Helpers;
using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace LovePdf.Model.TaskParams.Edit
{
    /// <summary>
    /// Text element
    /// </summary>
    public class TextElement : EditElement
    {
        private int fontSize;
        private string fontColor;
        private int letterSpacing;

        /// <summary>
        /// Construct TextElement
        /// </summary>
        public TextElement()
        {
            Type = ElementTypes.Text;
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Align = TextAligments.Left;
            FontFamily = FontFamilies.ArialUnicodeMs;
            FontSize = 14;
            FontStyle = TextFontStyles.Regular;
            FontColor = "#000000";
            LetterSpacing = 0;
            UnderlineText = false;
        }

        /// <summary>
        /// Text to be written in the document.
        /// </summary>
        [JsonProperty("text")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Text is required.")]
        public string Text { get; set; }

        /// <summary>
        /// Horizontal text alignment. Accepted values: left, center, right.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("text_align")]
        public TextAligments Align { get; set; }

        /// <summary>
        /// Font type of text.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("font_family")]
        public FontFamilies FontFamily { get; set; }

        /// <summary>
        /// Size of text font in pixels.
        /// </summary>
        [JsonProperty("font_size")]
        public int FontSize
        {
            get => fontSize;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(FontSize), "Font size must be a float greater than 0");
                }
                fontSize = value;
            }
        }

        /// <summary>
        /// Text font styles.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("font_style")]
        public TextFontStyles FontStyle { get; set; }

        /// <summary>
        /// Text font color. Can be set to transparent. Otherwise, the accepted value is hexadecimal.
        /// </summary> 
        [JsonProperty("font_color")] 
        public string FontColor
        {
            get => fontColor;
            set
            {
                if (ValidationHelper.IsValidColor(value) == false)
                {
                    throw new ArgumentException("Font color must be a 6-character hex value (e.g '#ABCDEF') or 'transparent'");
                }
                fontColor = value;
            }
        }

        /// <summary>
        /// Text letter spacing between characters. Accepted values from 0 to infinite.
        /// </summary>
        [JsonProperty("letter_spacing")]
        [Range(0, int.MaxValue, ErrorMessage = "Letter spacing must be a number greater or equal to 0")]
        public int LetterSpacing
        {
            get => letterSpacing;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(LetterSpacing), "Letter spacing must be a number greater or equal to 0");
                }
                letterSpacing = value;
            }
        }

        /// <summary>
        /// Text underline active or inactive. 
        /// </summary> 
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("underline_text")]
        public bool UnderlineText { get; set; }
    }
}
