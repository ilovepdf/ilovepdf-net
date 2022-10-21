using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Model.TaskParams.Sign
{
    public class ListRequest
    {
        private int perPage = 20;

        /// <summary>
        /// Lookup page
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; } = 0;

        /// <summary> 
        /// Paginator size
        /// Accepted values are in the range[1, 100]
        /// </summary> 
        [JsonProperty("per-page")]
        public int PerPage
        {
            get => perPage;
            set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(PerPage), "PerPage timeout must be between 1 and 100 pages");
                }
                perPage = value;
            }
        }

    }
}
