using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class SummarizeBasic
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create summarize task
            var task = api.CreateTask<SummarizeTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //process added files with summarize params
            var time = task.Process(new SummarizeParams
            {
                Language = "eng",
                OutputFormat = "md"
            });

            //download files to specific folder
            task.DownloadFile("/destination/folder/path");
        }
    }
}
