using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.Task;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class RotateBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create rotate task
            var task = api.CreateTask<RotateTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf", task.TaskId, Rotate.Degrees0);

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process();
            task.DownloadFile("path");
        }
    }
}