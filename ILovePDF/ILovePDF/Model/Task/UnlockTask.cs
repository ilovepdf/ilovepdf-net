using System;
using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    ///     Unlock PDFs
    /// </summary>
    public class UnlockTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.Unlock);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new UnlockParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(UnlockParams parameters)
        {
            if (parameters == null)
                parameters = new UnlockParams();

            return base.Process(parameters);
        }
    }
}