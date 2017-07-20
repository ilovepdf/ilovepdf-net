using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// Merge Pdf Documents
    /// </summary>
    public class MergeTask : LovePdfTask
    {
        /// <inheritdoc />
        public override string ToolName => EnumExtensions.GetEnumDescription(TaskName.Merge);

        /// <summary>
        /// Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new MergeParams();

            return base.Process(parameters);
        }

        /// <summary>
        /// Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(MergeParams parameters)
        {
            if (parameters == null)
                parameters = new MergeParams();

            return base.Process(parameters);
        }
    }
}
