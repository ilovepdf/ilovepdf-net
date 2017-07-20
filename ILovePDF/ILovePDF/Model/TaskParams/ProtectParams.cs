using Newtonsoft.Json;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// Protect Params
    /// </summary>
    public class ProtectParams : BaseParams {

        /// <summary>
        /// Password to lock a document
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password">password for the file</param>
        public ProtectParams(string password)
        {
            Password = password;
        }
    }
}
