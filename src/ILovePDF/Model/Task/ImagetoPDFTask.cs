using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Convert Images To PDF
    /// </summary>
    public class ImageToPdfTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.ImagePdf);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new ImageToPdfParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(ImageToPdfParams parameters)
        {
            if (parameters == null)
                parameters = new ImageToPdfParams();

            return base.Process(parameters);
        }
    }
}