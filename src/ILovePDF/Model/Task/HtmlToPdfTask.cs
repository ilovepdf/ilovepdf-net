using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// Html To Pdf
    /// </summary>
    public class HtmlToPdfTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.HtmlToPdf);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new HTMLtoPDFParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(PdftoJpgParams parameters)
        {
            if (parameters == null)
                parameters = new PdftoJpgParams();

            return base.Process(parameters);
        }
    }
}
