using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    /// Pdf To PdfA
    /// </summary>
    public class PdfToPdfATask : LovePdfTask
    {
        /// <inheritdoc />
        public override string ToolName => EnumExtensions.GetEnumDescription(TaskName.PdfToPdfA);

        /// <summary>
        /// Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new PdfToPdfAParams();

            return base.Process(parameters);
        }

        /// <summary>
        /// Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(PdfToPdfAParams parameters)
        {
            if (parameters == null)
                parameters = new PdfToPdfAParams();

            return base.Process(parameters);
        }
    }
}
