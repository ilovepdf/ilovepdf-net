using System;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     WaterMark Image Mode
    /// </summary>
    public class WatermarkModeImage
    {
        /// <summary>
        ///     Constructor for setting watermark Image
        /// </summary>
        /// <param name="serverFileName"></param>
        public WatermarkModeImage(String serverFileName)
        {
            ServerFileName = serverFileName;
        }

        /// <summary>
        ///     Server Filename To Add
        /// </summary>
        public String ServerFileName { get; }
    }
}