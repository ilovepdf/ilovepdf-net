using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// Add Page Numbers
    /// </summary>
    public class PageNumbersTask : LovePdfTask
    {
        /// <inheritdoc />
        public override string ToolName => EnumExtensions.GetEnumDescription(TaskName.PageNumber);

        /// <summary>
        /// Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new PageNumbersParams();

            return base.Process(parameters);
        }

        /// <summary>
        /// Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(PageNumbersParams parameters)
        {
            if (parameters == null)
                parameters = new PageNumbersParams();

            return base.Process(parameters);
        }
    }
}
