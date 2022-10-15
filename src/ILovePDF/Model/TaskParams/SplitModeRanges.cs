using System;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     Split Mode Ranges
    /// </summary>
    public class SplitModeRanges
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="ranges"></param>
        public SplitModeRanges(String ranges)
        {
            Ranges = ranges;
        }

        /// <summary>
        ///     Ranges
        /// </summary>
        public String Ranges { get; set; }
    }
}