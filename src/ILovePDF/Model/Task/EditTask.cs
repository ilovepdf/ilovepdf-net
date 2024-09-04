using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams;
using iLovePdf.Model.TaskParams.Edit;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace iLovePdf.Model.Task
{
    /// <summary>
    /// Try to edit PDFs
    /// </summary>
    public class EditTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.Edit);

        /// <summary>
        /// Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process(List<EditElement> elements)
        {
            var paramaters = new EditParams(elements);

            return base.Process(paramaters);
        }

        /// <summary>
        /// Process the task
        /// </summary>
        /// <param name="paramaters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(EditParams paramaters)
        {
            if (paramaters == null)
                paramaters = new EditParams(null);

            return base.Process(paramaters);
        }
    }
}