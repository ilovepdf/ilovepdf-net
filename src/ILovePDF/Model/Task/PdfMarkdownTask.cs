using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Pdf to Markdown
    /// </summary>
    public class PdfMarkdownTask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => "pdfmarkdown";

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new PdfMarkdownParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(PdfMarkdownParams parameters)
        {
            if (parameters == null)
                parameters = new PdfMarkdownParams();

            return base.Process(parameters);
        }
    }
}
