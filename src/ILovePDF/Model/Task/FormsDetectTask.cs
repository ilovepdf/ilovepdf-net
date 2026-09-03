using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Forms Detect
    /// </summary>
    public class FormsDetectTask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => "formsdetect";

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new FormsDetectParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(FormsDetectParams parameters)
        {
            if (parameters == null)
                parameters = new FormsDetectParams();

            return base.Process(parameters);
        }
    }
}
