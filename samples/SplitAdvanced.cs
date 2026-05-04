using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class SplitAdvanced
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create split task
            var task = api.CreateTask<SplitTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //proces added files
            //time var will contains information about time spent in process
            //Example of ranges split mode
            var time = task.Process
            (new SplitParams(new SplitModeRanges("2-4,6-8"))
            {
                OutputFileName = "split"
            });
            task.DownloadFile("path");

            //Example of fixed ranges split mode
            //var time = task.Process
            //(new SplitParams(new SplitModeFixedRanges(3))
            //{
            //    OutputFileName = "split"
            //});
            //task.DownloadFile("path");

            //Example of remove pages split mode
            //var time = task.Process
            //(new SplitParams(new SplitModeRemovePages("2-4,6-8"))
            //{
            //    OutputFileName = "split"
            //});
            //task.DownloadFile("path");


        }
    }
}