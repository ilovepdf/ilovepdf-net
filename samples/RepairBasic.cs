using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class RepairBasic
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

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