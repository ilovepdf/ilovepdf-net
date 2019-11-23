using System;
using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    ///     Extract
    /// </summary>
    public sealed class ExtractTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.Extract);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(ExtractParams parameters)
        {
            if (parameters == null)
                throw new ArgumentException("Parameters should not be null", nameof(parameters));

            return base.Process(parameters);
        }
    }
}