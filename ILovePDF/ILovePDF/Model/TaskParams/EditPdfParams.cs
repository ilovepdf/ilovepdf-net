using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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

            Elements.AddRange(elements);
        }

        /// <summary>
        ///     Position of the WaterMark above or below the original Pdf
        /// </summary>
        [JsonIgnore]
        public List<EditPdfParamsElementBase> Elements { get; } = new List<EditPdfParamsElementBase>();
    }
}