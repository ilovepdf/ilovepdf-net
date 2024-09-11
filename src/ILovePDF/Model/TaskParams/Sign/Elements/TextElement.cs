using iLovePdf.Model.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Model.TaskParams.Sign.Elements
{
    public class TextElement : BaseSignElement
    {
        public TextElement(string textContent, string pages, Position position, int size = 18)
                : base(SignElementTypes.Text, pages, position, size)
        {
            if (string.IsNullOrEmpty(textContent))
            {
                throw new ArgumentNullException(nameof(textContent), "Content can't be null or empty");
            }
             
            Text = textContent;  
        }

        /// <summary>
        /// It specifies the text that will appear on the text box. 
        /// /// </summary>
        [JsonProperty("content")]
        public string Text { get; set; }
    }
}
