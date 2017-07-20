namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// WaterMark Image Mode
    /// </summary>
    public class WatermarkModeImage
    {
        /// <summary>
        /// Server Filename To Add
        /// </summary>
        public string ServerFileName { get; }

        /// <summary>
        /// Constructor for setting watermark Image
        /// </summary>
        /// <param name="serverFileName"></param>
        public WatermarkModeImage(string serverFileName)
        {
            ServerFileName = serverFileName;
        }

    }
}