using iLovePdf.Model.Enums;
using Newtonsoft.Json;

namespace iLovePdf.Model.TaskParams.Edit
{
    /// <summary>
    /// Image element
    /// </summary>
    public class ImageElement : EditElement
    {
        /// <summary>
        /// Construct ImageElement
        /// </summary>
        public ImageElement()
        {
            Type = ElementTypes.Image;
        }

        /// <summary>
        /// Construct ImageElement
        /// </summary>
        /// <param name="serverFileName"></param>
        public ImageElement(string serverFileName) : this()
        {
            ServerFileName = serverFileName;
        }

        /// <summary>
        /// Upload resource that identifies the image. Accepted file formats: png, jpg, jpeg, jfif and gif.
        /// </summary>
        [JsonProperty("server_filename")]
        public string ServerFileName { get; set; }
    }
}
