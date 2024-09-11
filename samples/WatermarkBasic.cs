using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class WatermarkBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create watermark task
            var task = api.CreateTask<WaterMarkTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(new WaterMarkParams(new WatermarkModeText("text")));
            task.DownloadFile("path");
        }
    }
}