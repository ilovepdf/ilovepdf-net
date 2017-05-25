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
    public class ValidatepdfaBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create validate task
            var task = api.CreateTask<ValidatepdfaTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(new ValidatepdfaTaskParams());
            task.DownloadFile("path");
        }
    }
}
