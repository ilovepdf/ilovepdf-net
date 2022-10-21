using LovePdf.Model.TaskParams.Edit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// EditParams
    /// </summary>
    public class SignParams : BaseParams
    {
        /// <summary>
        /// Elements to be added into the PDF. They can have several properties including the element type.
        /// </summary> 
        [JsonIgnore]
        private List<EditElement> _elements = new List<EditElement>();

        public  SignParams(List<EditElement> elements)
        {
            var x = new SignParams(null);
        } 


    }
}