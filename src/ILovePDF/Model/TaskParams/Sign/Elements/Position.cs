using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using LovePdf.Model.TaskParams.Edit;

namespace LovePdf.Model.TaskParams.Sign.Elements
{
    /// <summary>
    /// X and Y position
    /// </summary>
    public class Position
    {
        private string x;
        private string y;

        /// <summary>
        /// Width of the element
        /// </summary>
        [JsonProperty("x")]
        [Required]
        [Range(0, float.MaxValue)]
        public string X
        {
            get => x;
            set
            {
                x = value;
            }
        }

        /// <summary>
        /// Height of the element
        /// </summary>
        [JsonProperty("y")]
        [Required]
        [Range(float.MinValue, 0)]
        public string Y
        {
            get => y;
            set
            {
                y = value;
            }
        } 

        /// <summary>
        /// Coordinate
        /// </summary>
        public Position(string x, string y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
