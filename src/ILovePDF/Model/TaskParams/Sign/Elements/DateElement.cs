using iLovePdf.Model.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace iLovePdf.Model.TaskParams.Sign.Elements
{
    public class DateElement : BaseSignElement
    {
        private string content;

        public DateElement(string dateContent, string pages, Position position, int size = 18) 
            : base(SignElementTypes.Date, pages, position, size)
        { 
            Content = dateContent;
        }

        /// <summary>
        /// It specifies the date format.
        /// <para> Allowed formats: "dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "yyyy.MM.dd", "MM-dd-yyyy", "MM/dd/yyyy", "MM.dd.yyyy"</para>
        /// </summary>
        [JsonProperty("content")]
        public string Content 
        { 
            get => content;
            set 
            { 
                string[] formats = {  
                    "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd",
                    "dd-MM-yyyy", "MM-dd-yyyy", "yyyy-MM-dd",
                    "dd.MM.yyyy", "MM.dd.yyyy", "yyyy.MM.dd"};
                DateTime expectedDate;
                if (!DateTime.TryParseExact(value, formats, new CultureInfo("en-US"),
                                            DateTimeStyles.None, out expectedDate) )
                {
                   // throw new ArgumentException("Invalid date format. Allowed formats: \"dd-MM-yyyy\", \"dd/MM/yyyy\", \"dd.MM.yyyy\", \"yyyy-MM-dd\", \"yyyy/MM/dd\", \"yyyy.MM.dd\", \"MM-dd-yyyy\", \"MM/dd/yyyy\", \"MM.dd.yyyy\"", nameof(Content));
                }
                content = value;    
            }
        }
    }
}
