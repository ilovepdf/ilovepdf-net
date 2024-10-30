using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class RepairAdvanced
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create specific task
            var task = api.CreateTask<RepairTask>();

            //fileResponse will contains property with server file name
            var fileResponse = task.AddFile("/path/to/file.pdf");

            //specify repeaired paramters like output filename
            var time = task.Process(new RepairParams {OutputFileName = "repaired_filename"});

            //download file and save to specific directory
            task.DownloadFile("/directory/to/store/file");
        }
    }
}