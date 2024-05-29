using LovePdf.Model.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LovePdf.Model.TaskParams.Sign.Elements
{
    public class InputElement : BaseSignElement
    { 
        public InputElement(string pages, Position position, int size = 18) 
            : base(SignElementTypes.Input, pages, position, size)
        { 
        } 

        /// <summary> 
        /// It must be a JSON string where you can define the attributes of that element, 
        /// an example is:
        /// <para>
        /// { "label": "my input label text", "description": "input description"}
        /// </para>   
        ///  </summary>
        [JsonProperty("info")]
        public string Info 
        { 
            get 
            {
                return JsonConvert.SerializeObject(new { label = Label, description = Description });
            }
        }

        /// <summary>
        /// Input label text
        /// </summary>
        [JsonIgnore]
        public string Label { set; get; }

        /// <summary>
        /// Input description
        /// </summary>
        [JsonIgnore]
        public string Description { set; get; }
    }
}
