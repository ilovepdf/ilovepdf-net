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
                    throw new ArgumentOutOfRangeException(nameof(X), "Invalid X value: it must be a number greater or equal to 0");
                }
                x = value;
            }
        }

        /// <summary>
        /// Height of the element
        /// </summary>
        [JsonProperty("y")]
        [Required]
        [Range(float.MinValue, 0)]
        public float Y
        {
            get => y;
            set
            {
                if (value > 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Y), "Invalid Y value: it must be a number lower or equal to 0");
                }
                y = value;
            }
        } 

        /// <summary>
        /// Coordinate
        /// </summary>
        public Position(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
