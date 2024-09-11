using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class RotateAdvanced
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create Rotate task
            var task = api.CreateTask<RotateTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf", task.TaskId, Rotate.Degrees90);

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(new RotateParams {OutputFileName = "rotated"});
            task.DownloadFile("path");
        }
    }
}