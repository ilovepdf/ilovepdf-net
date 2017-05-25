using ILovePDF;
using ILovePDF.Model.Task;
using ILovePDF.Model.TaskParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Samples
{
    public class TryCatchErrors
    {
        public void DoTask()
        {
            try
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
            catch(HttpRequestException ex)
            {
                var message = ex.Message;
                string innerException = string.Empty;
                if(ex.InnerException != null)
                {
                    innerException = ex.InnerException.Message;
                }
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                string innerException = string.Empty;
                if (ex.InnerException != null)
                {
                    innerException = ex.InnerException.Message;
                }
            }
        }
    }
}
