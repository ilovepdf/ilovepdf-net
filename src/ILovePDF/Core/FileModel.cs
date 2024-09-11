using System;
using iLovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iLovePdf.Core
{
    /// <summary>
    ///     File Model
    /// </summary>
    public class FileModel
    {
        /// <summary>
        ///     Server File name
        /// </summary>
        public String ServerFileName { get; set; }

        /// <summary>
        ///     File name
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        ///     Rotation
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Rotate Rotate { get; set; }

        /// <summary>
        ///     Password
        /// </summary>
        public String Password { get; set; }
    }
}