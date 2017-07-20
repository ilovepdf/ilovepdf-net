using System;
using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// Split Params
    /// </summary>
    public class SplitParams : BaseParams
    {
        /// <summary>
        /// Split Mode
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
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
            SplitMode = SplitModes.Ranges;
            Ranges = null;
            FixedRanges = 1;
            RemovePages = null;
            MergeAfter = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fixedRanges"></param>
        public SplitParams(SplitModeFixedRanges fixedRanges)
        {
            if (fixedRanges == null)
                throw new ArgumentException("cannot be null", nameof(fixedRanges));

            SetDefaultValues();
            SplitMode = SplitModes.FixedRange;
            FixedRanges = fixedRanges.FixedRange;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pages"></param>
        public SplitParams(SplitModeRemovePages pages)
        {
            if (pages == null)
                throw new ArgumentException("cannot be null", nameof(pages));

            SetDefaultValues();
            SplitMode = SplitModes.RemovePages;
            RemovePages = pages.RemovePages;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ranges"></param>
        public SplitParams(SplitModeRanges ranges)
        {
            if (ranges == null)
                throw new ArgumentException("cannot be null", nameof(ranges));

            SplitMode = SplitModes.Ranges;
            Ranges = ranges.Ranges;

        }
    }
}
