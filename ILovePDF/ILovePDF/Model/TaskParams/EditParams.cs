using LovePdf.Model.TaskParams.Edit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    /// EditParams
    /// </summary>
    public class EditParams : BaseParams
    {
        /// <summary>
        /// Elements to be added into the PDF. They can have several properties including the element type.
        /// </summary> 
        [JsonIgnore]
        public List<Element> Elements { get;  set; } = new List<Element>();
         
        /// <summary>
        /// Add new pdf element to list.
        /// </summary>
        /// <param name="element"></param>
        public void AddElement(Element element)
        {
            Elements.Add(element);
        }
         
    }
}