using LovePdf.Model.Enums;
using Newtonsoft.Json; 

namespace LovePdf.Model.TaskParams.Edit
{
    /// <summary>
    /// SVG element
    /// </summary>
    public class SvgElement : EditElement
    {
        /// <summary>
        /// Construct SvgElement
        /// </summary>
        public SvgElement()
        {
            Type = ElementTypes.SVG;
        }

        /// <summary>
        /// Construct SvgElement
        /// </summary>
        /// <param name="serverFileName"></param>
        public SvgElement(string serverFileName) : this()
        {
            ServerFileName = serverFileName;
        }

        /// <summary>
        /// Upload resource that identifies the image. Accepted file formats: svg.
        /// </summary>
        [JsonProperty("server_filename")]
        public string ServerFileName { get; set; }
    }
}
