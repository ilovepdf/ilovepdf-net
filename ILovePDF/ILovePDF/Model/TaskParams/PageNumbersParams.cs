using ILovePDF.Model.Enum.Params;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ILovePDF.Model.TaskParams
{
    public class PageNumbersParams : BaseParams
    {
        [JsonProperty("facing_pages")]
        public bool FacingPages { get; set; }

        [JsonProperty("first_cover")]
        public bool FirstCover { get; set; }

        [JsonProperty("pages")]
        public string Pages { get; set; }

        [JsonProperty("starting_number")]
        public int StartingNumber { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("vertical_position")]
        public VerticalPositions VerticalPosition { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("horizontal_position")]
        public HorizontalPositions HorisontalPosition { get; set; }

        [JsonProperty("vertical_position_adjustment")]
        public int VerticalPositionAdjustment { get; set; }

        [JsonProperty("horizontal_position_adjustment")]
        public int HorizontalPositionAdjustment { get; set; }

        [JsonProperty("font_family")]
        public string FontFamily { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("font_style")]
        public FontStyles? FontStyle { get; set; }

        [JsonProperty("font_size")]
        public int FontSize { get; set; }

        [JsonProperty("font_color")]
        public string FontColor { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public PageNumbersParams()
        {
            SetDefaultValues();
        }
        private void SetDefaultValues()
        {
            FacingPages = false;
            FirstCover = false;
            Pages = "all";
            StartingNumber = 1;
            VerticalPosition = VerticalPositions.bottom;
            HorisontalPosition = HorizontalPositions.middle;
            VerticalPositionAdjustment = 0;
            HorizontalPositionAdjustment = 0;
            FontFamily = "Arial Unicode MS";
            FontStyle = null;
            FontColor = "#000000";
            Text = "{n}";
        }
    }
}
