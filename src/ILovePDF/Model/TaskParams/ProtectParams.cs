using System;
using Newtonsoft.Json;

namespace iLovePdf.Model.TaskParams
{
    /// <summary>
    ///     Protect Params
    /// </summary>
    public class ProtectParams : BaseParams
    {
        /// <summary>
        /// </summary>
        /// <param name="password">password for the file</param>
        public ProtectParams(String password)
        {
            Password = password;
        }

        /// <summary>
        ///     Password to lock a document
        /// </summary>
        [JsonProperty("password")]
        public String Password { get; set; }
    }
}