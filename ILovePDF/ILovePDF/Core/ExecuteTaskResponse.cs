using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace LovePdf.Core
{
    /// <summary>
    ///     Execute Task Response
    /// </summary>
    public class ExecuteTaskResponse
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ExecuteTaskResponse()
        {
            Validations = new Collection<Validation>();
        }

        /// <summary>
        ///     Validations
        /// </summary>
        [JsonProperty("validations")]
        public Collection<Validation> Validations { get; }

        /// <summary>
        ///     FileSize
        /// </summary>
        [JsonProperty("filesize")]
        public Int64 FileSize { get; set; }

        /// <summary>
        ///     OutputFileSize
        /// </summary>
        [JsonProperty("output_filesize")]
        public Int64 OutputFileSize { get; set; }

        /// <summary>
        ///     Timer
        /// </summary>
        [JsonProperty("timer")]
        public Decimal Timer { get; set; }
    }
}