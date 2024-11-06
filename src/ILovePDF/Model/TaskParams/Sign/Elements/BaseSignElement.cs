using iLovePdf.Model.TaskParams.Edit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using iLovePdf.Model.TaskParams.Sign.Elements;
using Newtonsoft.Json.Converters;
using iLovePdf.Model.Enums;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace iLovePdf.Model.TaskParams.Sign.Elements
{
    public abstract class BaseSignElement : ISignElement
    {
        private static readonly string[] VALID_X_GRAVITY_POSITIONS = { "left", "center", "right" };
        private static readonly string[] VALID_Y_GRAVITY_POSITIONS = { "top", "center", "bottom" };
        private string pages;

        public BaseSignElement(SignElementTypes type, string pages, Position position, int size = 18)
        {
            this.Type = type;
            this.Pages = pages;
            this.Position = position;
            this.Size = size;
        }

        /// <summary>
        /// Type of element to be added.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        [Required]
        public SignElementTypes Type { get; private set; }  

        /// <summary>
        /// Page or pages where element will be placed.
        /// <para>Must have one of the following formats:</para>
        /// <para>"1"</para>
        /// <para>"3-12"</para>
        /// <para>"3,5,9-12"</para>
        /// </summary>
        [JsonProperty("pages")]
        [Required]
        public string Pages
        {
            get => pages;
            set
            {
                var input = value?.Trim()?.Replace(" ", "");
                if (string.IsNullOrEmpty(input) || !Regex.IsMatch(input, @"\d+(?:-\d+)?(?:,\d+(?:-\d+)?)*"))
                {
                    throw new ArgumentOutOfRangeException(nameof(Pages), 
                        "Pages must not be null and. Must have one of the following formats: \"1\", \"3-12\", \"3,5,9-12\".");
                } 
                pages = input;
            }
        }

        /// <summary>
        /// Position of the element in X and Y coordinates.
        /// </summary>  
        [JsonIgnore]
        public Position Position { get; set; }

        [JsonProperty("position")]
        public string PositionString => $"{Position.X} {Position.Y}";

        /// <summary>
        /// Element size. It corresponds to the height of the element. Width will adapt automatically according to its content
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Allows you to position the element on the left, in the center or on the right of the page.Accepted values: left, center, right.
        /// </summary>
        [JsonProperty("horizontal_adjustment")]
        public int HorizontalAdjustment { get; set; }

        /// <summary>
        /// Define if page element will be at top or bottom. Accepted values: bottom, top.
        /// </summary>
        [JsonProperty("vertical_adjustment")]
        public int VerticalAdjustment { get; set; }

        public void SetGravityPosition(string positionX, string positionY, int horizontalAdjustment = 0, int verticalAdjustment = 0)
        {
            if (Array.IndexOf(VALID_X_GRAVITY_POSITIONS, positionX) == -1)
            {
                throw new ArgumentException($"Invalid X value, valid positions are: {string.Join(", ", VALID_X_GRAVITY_POSITIONS)}");
            }

            if (Array.IndexOf(VALID_Y_GRAVITY_POSITIONS, positionY) == -1)
            {
                throw new ArgumentException($"Invalid Y value, valid positions are: {string.Join(", ", VALID_Y_GRAVITY_POSITIONS)}");
            }

            this.Position.X = positionX;
            this.Position.Y = positionY;
            this.HorizontalAdjustment = horizontalAdjustment;
            this.VerticalAdjustment = verticalAdjustment;
        }
    }

}
