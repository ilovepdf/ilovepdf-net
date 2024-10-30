﻿using System;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.TaskParams;

namespace iLovePdf.Model.Task
{
    /// <summary>
    ///     Pdf To Jpg
    /// </summary>
    public class PdfToJpgTask : iLovePdfTask
    {
        /// <inheritdoc />
        public override String ToolName => EnumExtensions.GetEnumDescription(TaskName.PdfToJpg);

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <returns></returns>
        public ExecuteTaskResponse Process()
        {
            var parameters = new PdftoJpgParams();

            return base.Process(parameters);
        }

        /// <summary>
        ///     Process the task
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ExecuteTaskResponse Process(PdftoJpgParams parameters)
        {
            if (parameters == null)
                parameters = new PdftoJpgParams();

            return base.Process(parameters);
        }
    }
}