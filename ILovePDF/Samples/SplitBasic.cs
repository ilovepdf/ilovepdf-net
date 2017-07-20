using LovePdf.Core;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;

namespace Samples
{
    public class SplitBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create split task
            var task = api.CreateTask<SplitTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(new SplitParams(new SplitModeFixedRanges(3)));
            task.DownloadFile("path");
        }
    }
}
