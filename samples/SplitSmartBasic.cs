using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class SplitSmartBasic
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create splitsmart task
            var task = api.CreateTask<SplitSmartTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //process added files with prompt
            var time = task.Process(new SplitSmartParams
            {
                Prompt = "Split this PDF into chapters based on headings"
            });

            //download files to specific folder
            task.DownloadFile("/destination/folder/path");
        }
    }
}
