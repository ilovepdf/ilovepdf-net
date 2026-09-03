using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class TranslateBasic
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create translate task
            var task = api.CreateTask<TranslateTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //process added files with translate params
            var time = task.Process(new TranslateParams
            {
                LanguageOutput = "spa",
                TranslateMode = "layout"
            });

            //download files to specific folder
            task.DownloadFile("/destination/folder/path");
        }
    }
}
