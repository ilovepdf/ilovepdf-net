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
    public class EditParams : BaseParams
    {
        /// <summary>
        /// Elements to be added into the PDF. They can have several properties including the element type.
        /// </summary> 
        [JsonIgnore]
        private List<EditElement> _elements;

        public EditParams(List<EditElement> elements)
        {
            SetElements(elements);
        }

        private EditParams()
        {
        }

        public static EditParams New()
        {
            return new EditParams();
        }

        [JsonIgnore]
        public List<EditElement> Elements => _elements;

        public void SetElements(List<EditElement> elements)
        {
            if (elements == null || elements.Count == 0)
            {
                throw new ArgumentException(
                    $"Editpdf task should have at least one element (text, image or svg).");
            }
            _elements = elements;
        }

        public EditElement AddElement(EditElement element)
        {
            _elements.Add(element);
            return _elements.First(x => x == element);
        }

        public TextElement AddText(string text)
        {
            var element = new TextElement()
            {
                Text = text
            };
            _elements.Add(element);
            return _elements.First(x => x == element) as TextElement;
        }

        public ImageElement AddImage(string serverFileName)
        {
            var element = new ImageElement(serverFileName);
            _elements.Add(element);
            return (ImageElement)_elements.First(x => x == element);
        }

        public SvgElement AddSvg(string serverFileName)
        {
            var element = new SvgElement(serverFileName);
            _elements.Add(element);
            return (SvgElement)_elements.First(x => x == element);
        }
    }
}