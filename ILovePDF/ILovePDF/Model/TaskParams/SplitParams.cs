using ILovePDF.Model.Enum.Params;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ILovePDF.Model.TaskParams
{
    public class SplitParams : BaseParams
    {
        [JsonConverter(typeof (StringEnumConverter))]
        [JsonProperty("split_mode")]
        public SplitModes SplitMode { get; set; }

        /// <summary>
        /// Example format 1,5,10-14
        /// </summary>
        [JsonProperty("ranges")]
        public string Ranges { get; set; }

        /// <summary>
        /// Split the PDF file in chunks by every defined number. 
        /// </summary>
        [JsonProperty("fixed_range")]
        public int FixedRanges { get; set; }

        /// <summary>
        /// Pages to remove from a PDF. Accepted format: 1,4,8-12,16. 
        /// </summary>
        [JsonProperty("remove_pages")]
        public string RemovePages { get; set; }

        /// <summary>
        /// Merge all ranges after being split. This param takes effect only when {mode} is range. 
        /// Default: false
        /// </summary>
        [JsonProperty("merge_after")]
        public bool MergeAfter { get; set; }

        private void SetDefaultValues()
        {
            SplitMode = SplitModes.ranges;
            Ranges = null;
            FixedRanges = 1;
            RemovePages = null;
            MergeAfter = false;
        }

        public SplitParams(SplitModeFixedRanges fixedRanges)
        {
            SetDefaultValues();
            SplitMode = SplitModes.fixed_range;
            FixedRanges = fixedRanges.FixedRange;
        }


        public SplitParams(SplitModeRemovePages pages)
        {
            SetDefaultValues();
            SplitMode = SplitModes.remove_pages;
            RemovePages = pages.RemovePages;
        }

        public SplitParams(SplitModeRanges ranges)
        {
            SplitMode = SplitModes.ranges;
            Ranges = ranges.Ranges;

        }
    }

    public class SplitModeRanges
    {
        public string Ranges { get; set; }

        public SplitModeRanges(string ranges)
        {
            Ranges = ranges;
        }


    }

    public class SplitModeFixedRanges
    {
        public int FixedRange { get; set; }

        /// <summary>
        /// Split the PDF file in chunks by every defined number. 
        /// </summary>
        /// <param name="ranges"></param>
        public SplitModeFixedRanges(int ranges)
        {
            FixedRange = ranges;
        }
    }

    public class SplitModeRemovePages
    {
        public string RemovePages { get; set; }

        /// <summary>
        /// Pages to remove from a PDF. Accepted format: 1,4,8-12,16. 
        /// </summary>
        /// <param name="removePages">Accepted format: 1,4,8-12,16. </param>
        public SplitModeRemovePages(string removePages)
        {
            RemovePages = removePages;
        }
    }

}
