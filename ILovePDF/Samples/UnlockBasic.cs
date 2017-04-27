using ILovePDF;
using ILovePDF.Model.Task;


namespace Samples
{
    public class UnlockBasic
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
            //time var will contains information about time spent in process
            var time = task.Process();
            task.DownloadFile("path");
        }
    }
}
