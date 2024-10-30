﻿using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Validate PDFA compliance
    /// </summary>
    public class ValidatePdfATask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.ValidatePdfA);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(ValidatePdfAParams parameters)
        {
            if (parameters == null)
                throw new ArgumentException("Parameters should not be null", nameof(parameters));

            return base.Process(parameters);
        }
    }
}