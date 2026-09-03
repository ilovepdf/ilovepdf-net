using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Translate PDFs
    /// </summary>
    public class TranslateTask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => "translate";

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new TranslateParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(TranslateParams parameters)
        {
            if (parameters == null)
                parameters = new TranslateParams();

            return base.Process(parameters);
        }
    }
}
