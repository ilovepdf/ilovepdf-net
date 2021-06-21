using System;
using Newtonsoft.Json;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EditPdfParamsElementImage : EditPdfParamsElementBase
    {
        /// <inheritdoc />
        public override String Type => "image";

        /// <summary>
        ///     Constructor for adding image to PDF
        /// </summary>
        /// <param name="serverFileName"></param>
        public EditPdfParamsElementImage(String serverFileName)
        {
            ServerFileName = serverFileName;
        }

        /// <summary>
        ///     Upload resource that identifies the image. Accepted file formats: png, jpg, jpeg, jfif and gif.
        /// </summary>
        [JsonProperty("server_filename", Required = Required.Always)]
        public String ServerFileName { get; }
    }
}