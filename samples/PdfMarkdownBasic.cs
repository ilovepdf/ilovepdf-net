using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class PdfMarkdownBasic
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create pdfmarkdown task
            var task = api.CreateTask<PdfMarkdownTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            //process added files
            var time = task.Process();

            //download files to specific folder
            task.DownloadFile("/destination/folder/path");
        }
    }
}
