
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// Try to repair PDFs
    /// </summary>
    public class RepairTask : LovePdfTask
    {
        /// <inheritdoc />
        public override string ToolName => EnumExtensions.GetEnumDescription(TaskName.Repair);

        /// <summary>
        /// Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var paramaters = new RepairParams();

            return base.Process(paramaters);
        }

        /// <summary>
        /// Process the task
        /// </summary>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(RepairParams paramaters)
        {
            if (paramaters == null)
                paramaters = new RepairParams();

            return base.Process(paramaters);
        }
    }
}
