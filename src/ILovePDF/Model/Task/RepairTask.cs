using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Try to repair PDFs
    /// </summary>
    public class RepairTask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.Repair);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var paramaters = new RepairParams();

            return base.Process(paramaters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(RepairParams paramaters)
        {
            if (paramaters == null)
                paramaters = new RepairParams();

            return base.Process(paramaters);
        }
    }
}