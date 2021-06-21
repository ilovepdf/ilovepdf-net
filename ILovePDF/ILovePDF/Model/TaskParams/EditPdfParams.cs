using System;
using System.Collections.Generic;
using LovePdf.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LovePdf.Model.TaskParams
{
    /// <summary>
    ///     EditPdfParams
    /// </summary>
    public class EditPdfParams : BaseParams
    {
        /// <summary>
        ///     Params
        /// </summary>
        /// <param name="elements"></param>
        public EditPdfParams(IEnumerable<EditPdfParamsElementBase> elements)
        {
            if (elements == null)
                throw new ArgumentException("cannot be null", nameof(elements));

            if (Elements.Count == 0)
                throw new ArgumentException("cannot be empty", nameof(elements));

            Elements.AddRange(elements);
        }

        /// <summary>
        ///     Position of the WaterMark above or below the original Pdf
        /// </summary>
        [JsonIgnore]
        public List<EditPdfParamsElementBase> Elements { get; } = new List<EditPdfParamsElementBase>();
    }
}