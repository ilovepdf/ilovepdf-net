using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Edit;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class EditBasic
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            // Create edit task
            var task = api.CreateTask<EditTask>();
            
            // File variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            // Upload Image file to Ilovepdf servers
            var imageFile = task.AddFile("your_image.jpg");

            // Create ImageElement
            var imageElement = new ImageElement(imageFile.ServerFileName);
           
            var elements = new List<EditElement>();
            elements.Add(imageElement);

            // Create edit task params
            var editParams = new EditParams(elements);
             
            editParams.OutputFileName = "editpdf-basic";
            
            // Proces added files,
            // time var will contains information about time spent in process
            var time = task.Process(editParams);
            task.DownloadFile("path");
        }
    }
}