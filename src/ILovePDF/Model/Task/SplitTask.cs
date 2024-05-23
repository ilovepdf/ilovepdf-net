﻿using System;
using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.TaskParams;

namespace LovePdf.Model.Task
{
    /// <summary>
    ///     Split PDFs
    /// </summary>
    public class SplitTask : LovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.Split);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(SplitParams parameters)
        {
            if (parameters == null)
                throw new ArgumentException("Parameters should not be null", nameof(parameters));

            return base.Process(parameters);
        }
    }
}