using ILovePDF;
using ILovePDF.Model.Task;
using ILovePDF.Model.TaskParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
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
            var time = task.Process(new WatermarkParams(new WatermarkModeText("text")));
            task.DownloadFile("path");
        }
    }
}
