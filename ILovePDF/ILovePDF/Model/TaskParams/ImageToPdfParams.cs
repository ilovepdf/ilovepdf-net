using ILovePDF.Model.Enum.Params;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ILovePDF.Model.TaskParams
{
    public class ImageToPdfParams : BaseParams
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("orientation")]
        public Orientations Orientation { get; set; }

        [JsonProperty("margin")]
        public int Margin { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("pagesize")]
        public PageSizes PageSize { get; set; }

        [JsonProperty("merge_after")]
        public bool MergeAfter { get; set; }

        public ImageToPdfParams()
        {
            SetDefaultValues();
        }
        private void SetDefaultValues()
        {
            this.Orientation = Orientations.portrait;
            this.Margin = 0;
            this.PageSize = PageSizes.fit;
            this.MergeAfter = true;
        }
    }


}
