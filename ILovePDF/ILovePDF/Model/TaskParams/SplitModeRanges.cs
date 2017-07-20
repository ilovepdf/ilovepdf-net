namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// Split Mode Ranges
    /// </summary>
    public class SplitModeRanges
    {
        /// <summary>
        /// Ranges
        /// </summary>
        public string Ranges { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ranges"></param>
        public SplitModeRanges(string ranges)
        {
            Ranges = ranges;
        }
    }
}