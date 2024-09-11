using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Add Page Numbers
    /// </summary>
    public class PageNumbersTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.PageNumber);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new PageNumbersParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(PageNumbersParams parameters)
        {
            if (parameters == null)
                parameters = new PageNumbersParams();

            return base.Process(parameters);
        }
    }
}