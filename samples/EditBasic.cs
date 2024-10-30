using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;
using iLovePdf.Model.TaskParams.Edit;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class EditBasic
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            // Create edit task
            var task = api.CreateTask<EditTask>();

            // File variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            // Upload Image file to Ilovepdf servers
            var imageFile = task.AddFile("your_image.jpg");

            // Create task params
            var editParams = new EditParams();

            // Create ImageElement
            var imageElement = editParams.AddImage(imageFile.ServerFileName);
           
            editParams.OutputFileName = "editpdf-basic";
            
            // Proces added files,
            // time var will contains information about time spent in process
            var time = task.Process(editParams);
            task.DownloadFile("path");
        }
    }
}