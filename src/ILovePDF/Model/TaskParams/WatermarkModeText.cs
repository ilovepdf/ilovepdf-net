using System;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    ///     WaterMark Text Mode
    /// </summary>
    public class WatermarkModeText
    {
        /// <summary>
        ///     Constructor for setting watermark text
        /// </summary>
        /// <param name="text"></param>
        public WatermarkModeText(String text)
        {
            Text = text;
        }

        /// <summary>
        ///     Text to show
        /// </summary>
        public String Text { get; }
    }
}