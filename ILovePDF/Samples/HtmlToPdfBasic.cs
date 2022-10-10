using System;
using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class HtmlToPdfBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create protect task
            var task = api.CreateTask<HtmlToPdfTask>();

            //file variable contains server file name
            var file = task.AddFile(new Uri("https://ilovepdf.com"));

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(new HTMLtoPDFParams());
            task.DownloadFile("path");
        }
    }
}