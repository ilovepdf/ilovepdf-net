using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace LovePdf.Core
{
    /// <summary>
    /// Execute Task Response
    /// </summary>
    public class ExecuteTaskResponse
    {
        /// <summary>
        /// Validations
        /// </summary>
        [JsonProperty("validations")]
        public Collection<Validation> Validations { get; }
        /// <summary>
        /// FileSize
        /// </summary>
        [JsonProperty("filesize")]
        public long FileSize { get; set; }
        /// <summary>
        /// OutputFileSize
        /// </summary>
        [JsonProperty("output_filesize")]
        public long OutputFileSize { get; set; }
        /// <summary>
        /// Timer
        /// </summary>
        [JsonProperty("timer")]
        public decimal Timer { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ExecuteTaskResponse()
        {
            Validations = new Collection<Validation>();
        }
    }
}