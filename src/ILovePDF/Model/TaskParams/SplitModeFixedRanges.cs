using System;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    ///     Split Mode Fixed Ranges
    /// </summary>
    public class SplitModeFixedRanges
    {
        /// <summary>
        ///     Split the PDF file in chunks by every defined number.
        /// </summary>
        /// <param name="ranges"></param>
        public SplitModeFixedRanges(Int32 ranges)
        {
            FixedRange = ranges;
        }

        /// <summary>
        ///     Fixed Range
        /// </summary>
        public Int32 FixedRange { get; set; }
    }
}