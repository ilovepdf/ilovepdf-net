using System;
using Newtonsoft.Json;

namespace iLovePdf.Model.TaskParams
{
    public class ExtractParams : BaseParams
    {
        /// <summary>
        ///     Detailed
        /// </summary>
        [JsonProperty("detailed")]
        public Boolean Detailed { get; set; }

        /// <summary>
        ///     Detailed
        /// </summary>
        [JsonProperty("by_word")]
        public Boolean ByWord { get; set; }
    }
}