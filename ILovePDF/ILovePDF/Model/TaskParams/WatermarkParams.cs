using ILovePDF.Model.Enum.Params;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ILovePDF.Model.TaskParams
{
    public class WatermarkParams : BaseParams
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("mode")]
        public WatermarkModes Mode { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("pages")]
        public string Pages { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("vertical_position")]
        public WatermarkVerticalPositions? VerticalPosition { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("horizontal_position")]
        public WatermarkHorizontalPositions? HorizontalPosition { get; set; }

        [JsonProperty("vertical_position_adjustment")]
        public int VerticalPositionAdjustment { get; set; }

        [JsonProperty("horizontal_position_adjustment")]
        public int HorizontalPositionAdjustment { get; set; }

        [JsonProperty("mosaic")]
        public bool Mosaic { get; set; }

        [JsonProperty("rotation")]
        public int Rotation { get; set; }

        [JsonProperty("font_family")]
        public string FontFamily { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("font_style")]
        public FontStyles? FontStyle { get; set; }

        [JsonProperty("font_size")]
        public int FontSize { get; set; }

        [JsonProperty("font_color")]
        public string FontColor { get; set; }

        [JsonProperty("transparency")]
        public int Transparency { get; set; }

        [JsonProperty("layer")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Layer? Layer { get; set; }

        public WatermarkParams(WatermarkModeText mode)
        {
            SetDefaultValues();
            Mode = WatermarkModes.text;
            Text = mode.Text;

        }

        public WatermarkParams(WatermarkModeImage mode)
        {
            SetDefaultValues();
            Mode = WatermarkModes.image;
            Image = mode.ServerFileName;
        }


        private void SetDefaultValues()
        {
            Mode = WatermarkModes.text;
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

    public class WatermarkModeText
    {
        public string Text { get; }
        /// <summary>
        /// Constructor for setting watermark text
        /// </summary>
        /// <param name="text"></param>
        public WatermarkModeText(string text)
        {
            Text = text;
        }
    }

}

public class WatermarkModeImage
{
    public string ServerFileName { get; }

    public WatermarkModeImage(string serverFileName)
    {
        ServerFileName = serverFileName;
    }

}
