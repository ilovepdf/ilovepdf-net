using ILovePDF;
using ILovePDF.Model.Task;

namespace Samples
{
    public class CompressBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");
            
            //create compress task
            var task = api.CreateTask<CompressTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process();


            //download files to specific folder
            task.DownloadFile("/destination/folder/path");

        }
    }
}
