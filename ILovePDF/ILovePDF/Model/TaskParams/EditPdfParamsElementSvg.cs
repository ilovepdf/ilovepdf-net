using System;
using Newtonsoft.Json;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditPdfParamsElementSvg : EditPdfParamsElementBase
    {
        /// <inheritdoc />
        public override String Type => "svg";

        /// <summary>
        ///     Constructor for adding SVG to PDF
        /// </summary>
        /// <param name="serverFileName"></param>
        public EditPdfParamsElementSvg(String serverFileName)
        {
            ServerFileName = serverFileName;
        }

        /// <summary>
        ///     Upload resource that identifies the image. Accepted file formats: svg
        /// </summary>
        [JsonProperty("server_filename", Required = Required.Always)]
        public String ServerFileName { get; }
    }
}