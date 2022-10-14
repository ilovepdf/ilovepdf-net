using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace LovePdf.Model.TaskParams.Edit
{
    /// <summary>
    /// Class Element
    /// </summary>
    public class Element
    {
        private int rotation;
        private int opacity = 100;
        private int pages = 1; 

        /// <summary>
        /// Type of element to be added.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        [Required]
        public ElementTypes Type { get; protected set; } = ElementTypes.Bottom;

        /// <summary>
        /// The page where the element will be inserted. Only numbers format is allowed.
        /// </summary>
        [JsonProperty("pages")]
        [Required]
        public int Pages
        {
            get => pages;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Pages must be no less than 1.");
                }
                pages = value;
            }
        }

        /// <summary>
        /// Depth value to decide elements order in case of overlay.
        /// </summary>
        [JsonProperty("zindex")]
        public int ZIndex { get; set; } = 1;

        /// <summary>
        /// Size of the element.
        /// </summary>
        [JsonIgnore]
        public Dimension Dimensions { get; set; }

        /// <summary>
        /// Position of the element in X and Y coordinates.
        /// </summary>
        [JsonIgnore]
        public Coordinate Coordinates { get; set; }

        /// <summary>
        /// Angle of rotation. Accepted integer range: 0-360.
        /// </summary>
        [JsonProperty("rotation")]
        [Range(0, 360, ErrorMessage = "Rotation must be an integer between 0 and 360")]
        public int Rotation
        {
            get => rotation;
            set
            {
                if (value < 0 || value > 360)
                {
                    throw new ArgumentOutOfRangeException("Rotation must be an integer between 0 and 360");
                }
                rotation = value;
            }
        }

        /// <summary>
        /// Percentage of opacity for stamping element. Accepted integer range 1-100.
        /// </summary>
        [JsonProperty("opacity")]
        [Range(0, 100, ErrorMessage = "Opacity must be an integer between 0 and 100")]
        public int Opacity
        {
            get => opacity;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Opacity must be an integer between 0 and 100");
                }
                opacity = value;
            }
        }
    }
}
