using LovePdf.Core;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;

namespace Samples
{
    public class ValidatepdfaBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create validate task
            var task = api.CreateTask<ValidatePdfATask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(new ValidatePdfAParams());
            task.DownloadFile("path");
        }
    }
}
