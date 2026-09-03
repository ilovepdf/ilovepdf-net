using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Split Smart PDFs using prompt
    /// </summary>
    public class SplitSmartTask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => "splitsmart";

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new SplitSmartParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task with parameters
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(SplitSmartParams parameters)
        {
            if (parameters == null)
                parameters = new SplitSmartParams();

            return base.Process(parameters);
        }
    }
}
