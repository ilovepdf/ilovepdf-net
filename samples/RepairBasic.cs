﻿using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Task;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class RepairBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            var task = api.CreateTask<RepairTask>();

            //file var contains information about server file name
            var file = task.AddFile("pat/to/file.pdf");

            // process files
            // time var will have info about time spent in process
            var time = task.Process();

            //download files to specific directory
            task.DownloadFile("/directory/to/save/files");
        }
    }
}