using LovePdf.Model.TaskParams.Edit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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
        private List<EditElement> _elements = new List<EditElement>();

        [JsonIgnore]
        public List<EditElement> Elements => _elements;

        public EditParams(EditParamBuilder builder)
        {
            if (builder?.Elements == null || builder.Elements.Count == 0)
            {
                throw new ArgumentException(
                    $"Editpdf task should have at least one element (text, image or svg).");
            }
            _elements = builder.Elements;
        } 
    }
}