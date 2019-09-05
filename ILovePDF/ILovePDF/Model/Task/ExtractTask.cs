using System;
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// Extract
    /// </summary>
    public sealed class ExtractTask : LovePdfTask
    {
        /// <inheritdoc />
        public override string ToolName => EnumExtensions.GetEnumDescription(TaskName.Extract);

        /// <summary>
        /// Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(ExtractParams parameters)
        {
            if (parameters == null)
                throw new ArgumentException("Parameters should not be null", nameof(parameters));

            return base.Process(parameters);
        }
    }
}
