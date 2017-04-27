using ILovePDF;
using ILovePDF.Model.Task;
using ILovePDF.Model.TaskParams;

namespace Samples
{
    public class UnlockAdvanced
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create unlock task
            var task = api.CreateTask<UnlockTask>();

            //file variable contains server file name
            // set the password witch the document is locked
            var file = task.AddFile("path/to/file/document.pdf", password: "test");

            //proces added files
            //time var will contais information about time spent in process
            var time = task.Process(new UnlockParams { OutputFileName = "unloked"});
            task.DownloadFile("path");
        }
    }
}
