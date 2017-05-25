using ILovePDF.Model.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILovePDF.Model.TaskParams
{
    public class ValidatepdfaTaskParams : BaseParams
    {
        /// <summary>
        /// Accepted values in ConformanceValues (pdfa-1b, pdfa-1a, pdfa-2b, pdfa-2u, pdfa-2a, pdfa-3b, pdfa-3u, pdfa-3a)
        /// </summary>
        [JsonProperty("conformance")]
        public string Conformance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ValidatepdfaTaskParams()
        {
            SetDefaultValues();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conformance">conformance level for pdf file</param>
        public ValidatepdfaTaskParams(ConformanceValues conformance)
        {
            Conformance = this.GetEnumDescription(conformance);
        }
        

        private void SetDefaultValues()
        {
            Conformance = this.GetEnumDescription(ConformanceValues.Pdfa1b);
        }
    }
}
