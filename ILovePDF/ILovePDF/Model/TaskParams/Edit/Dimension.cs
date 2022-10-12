using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LovePdf.Model.TaskParams.Edit
{
    /// <summary>
    ///  Size of the element
    /// </summary>
    public class Dimension
    {
        private float width;
        private float height;

        /// <summary>
        /// Width of the element
        /// </summary>
        [JsonProperty("w")]
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Element width must be a number greater or equal to 0")]
        public float Width
        {
            get => width;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Element width must be a number greater or equal to 0");
                }
                width = value;
            }
        }

        /// <summary>
        /// Height of the element
        /// </summary>
        [JsonProperty("h")]
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Element height must be a number greater or equal to 0")]
        public float Height
        {
            get => height;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Element height must be a number greater or equal to 0");
                }
                height = value;
            }
        }  

        /// <summary>
        /// Dimension
        /// </summary>
        public Dimension(float width, float height)
        {
            this.Height = width;
            this.Width = height;
        }
    }
}
