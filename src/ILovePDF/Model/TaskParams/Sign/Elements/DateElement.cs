using LovePdf.Model.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace LovePdf.Model.TaskParams.Sign.Elements
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
        /// <para> Allowed formats: "dd-MM-YYYY", "dd/MM/YYYY", "dd.MM.YYYY", "YYYY-MM-dd", "YYYY/MM/dd", "YYYY.MM.dd", "MM-dd-YYYY", "MM/dd/YYYY", "MM.dd.YYYY"</para>
        /// </summary>
        [JsonProperty("content")]
        public string Content 
        { 
            get => content;
            set 
            {
                string[] formats = { "dd-MM-YYYY", "dd/MM/YYYY", "dd.MM.YYYY", "YYYY-MM-dd", "YYYY/MM/dd", "YYYY.MM.dd", "MM-dd-YYYY", "MM/dd/YYYY", "MM.dd.YYYY" };
                DateTime expectedDate;
                if (!DateTime.TryParseExact(value, formats, new CultureInfo("en-US"),
                                            DateTimeStyles.None, out expectedDate))
                {
                    throw new ArgumentException("Invalid date format. Allowed formats: \"dd-MM-YYYY\", \"dd/MM/YYYY\", \"dd.MM.YYYY\", \"YYYY-MM-dd\", \"YYYY/MM/dd\", \"YYYY.MM.dd\", \"MM-dd-YYYY\", \"MM/dd/YYYY\", \"MM.dd.YYYY\"", nameof(Content));
                }
                content = value;    
            }
        }
    }
}
