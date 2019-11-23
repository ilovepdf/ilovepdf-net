using System;
using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    ///     Rotate PDFs
    /// </summary>
    public class RotateTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.Rotate);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new RotateParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(RotateParams parameters)
        {
            if (parameters == null)
                parameters = new RotateParams();

            return base.Process(parameters);
        }
    }
}