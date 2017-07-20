namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// WaterMark Text Mode
    /// </summary>
    public class WatermarkModeText
    {
        /// <summary>
        /// Text to show
        /// </summary>
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