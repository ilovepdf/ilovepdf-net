using ILovePDF;
using ILovePDF.Model.Enum;
using ILovePDF.Model.Task;
using ILovePDF.Model.TaskParams;

namespace Samples
{
    public class RotateAdvanced
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create Rotate task
            var task = api.CreateTask<RotateTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf", rotate: Rotate._90);

            //proces added files
            //time var will contais information about time spent in process
            var time = task.Process(new RotateParams() { OutputFileName = "rotated"});
            task.DownloadFile("path");
        }
    }
}
