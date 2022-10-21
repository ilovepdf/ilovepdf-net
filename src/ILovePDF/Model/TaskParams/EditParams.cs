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
        public List<EditElement> Elements { get; private set; } = new List<EditElement>();

        public EditParams(List<EditElement> elements = null)
        {
            if (elements != null) 
            {
                Elements = elements;
            }
        }

        public EditElement AddElement(EditElement element)
        {
            Elements.Add(element);
            return element;
        }

        public TextElement AddText(string text)
        {
            var element = new TextElement()
            {
                Text = text
            };
            Elements.Add(element);
            return element;
        }

        public ImageElement AddImage(string serverFileName)
        {
            var element = new ImageElement(serverFileName);
            Elements.Add(element);
            return element;
        }

        public SvgElement AddSvg(string serverFileName)
        {
            var element = new SvgElement(serverFileName);
            Elements.Add(element);
            return element;
        }

        public void Clear()
        {
            Elements.Clear();
        }
    }
}