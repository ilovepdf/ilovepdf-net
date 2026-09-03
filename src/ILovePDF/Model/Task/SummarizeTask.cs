using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Summarize PDFs
    /// </summary>
    public class SummarizeTask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => "summarize";

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new SummarizeParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(SummarizeParams parameters)
        {
            if (parameters == null)
                parameters = new SummarizeParams();

            return base.Process(parameters);
        }
    }
}
