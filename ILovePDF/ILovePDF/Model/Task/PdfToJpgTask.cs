using System;
using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    ///     Pdf To Jpg
    /// </summary>
    public class PdfToJpgTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.PdfToJpg);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new PdftoJpgParams();

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