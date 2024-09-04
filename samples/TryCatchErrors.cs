using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
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
            catch (HttpRequestException ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    var innerException = ex.InnerException.Message;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    var innerException = ex.InnerException.Message;
                }
            }
        }
    }
}