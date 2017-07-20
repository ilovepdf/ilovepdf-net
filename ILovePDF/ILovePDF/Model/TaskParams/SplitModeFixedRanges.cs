namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// Split Mode Fixed Ranges
    /// </summary>
    public class SplitModeFixedRanges
    {
        /// <summary>
        /// Fixed Range
        /// </summary>
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
}