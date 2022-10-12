using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LovePdf.Model.TaskParams.Edit
{
    /// <summary>
    /// X and Y coordinates
    /// </summary>
    public class Coordinate
    {
        private float y;
        private float x;

        /// <summary>
        /// Width of the element
        /// </summary>
        [JsonProperty("x")]
        [Required]
        [Range(0, float.MaxValue)]
        public float X
        {
            get => x;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Coordinate X must be a number greater or equal to 0");
                }
                x = value;
            }
        }

        /// <summary>
        /// Height of the element
        /// </summary>
        [JsonProperty("y")]
        [Required]
        [Range(0, float.MaxValue)]
        public float Y
        {
            get => y;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Coordinate Y must be a number greater or equal to 0");
                }
                y = value;
            }
        } 

        /// <summary>
        /// Coordinate
        /// </summary>
        public Coordinate(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
