using iLovePdf.Model.TaskParams.Sign.Elements;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace iLovePdf.Model.TaskParams.Sign.Signers
{
    /// <summary>
    /// File that a receiver of type signer needs to sign.
    /// </summary>
    public partial class SignerFile
    {
        /// <summary> 
        /// Server filename of an uploaded PDF file.  
        /// </summary>
        [JsonProperty("server_filename")]
        public string ServerFilename { get; set; }
         
        /// <summary>
        /// Definition of the elements in a PDF document.
        /// </summary>  
        [JsonIgnore]
        public List<ISignElement> Elements { get; private set; } = new List<ISignElement>();
    }
}
