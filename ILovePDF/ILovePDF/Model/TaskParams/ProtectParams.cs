using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILovePDF.Model.TaskParams
{
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
