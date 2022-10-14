using System.Diagnostics.CodeAnalysis;
using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;
using LovePdf.Model.TaskParams.Edit;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class EditAdvanced
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            // Create edit task
            var task = api.CreateTask<EditTask>();
            
            // File variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");

            // Upload Image files to Ilovepdf servers
            var imageFile = task.AddFile("your_image.jpg");
            var svgFile = task.AddFile("your_image.svg");

            // Create ImageElement
            var imageElement = new ImageElement()
            {
                Coordinates = new Coordinate(300, 600),
                Pages = 3,
                Opacity = 40,
                ServerFileName = imageFile.ServerFileName
            };

            // Create TextElement
            var textElement = new TextElement()
            {
                Text = "This is a sample text",
                Coordinates = new Coordinate(300, 600),
                Pages = 2,
                Align = TextAligments.Center,
                FontFamily = FontFamilies.TimesNewRoman,
                FontColor = "#FB8B24", // Orange
                FontStyle = TextFontStyles.Bold
            };

            // Create SvgElement
            var svgElement = new SvgElement(svgFile.ServerFileName);
             
            // Create edit task params
            var editParams = new EditParams();

            // Add elements to Editpdf task in order of drawing (important if elements overlap!)
            editParams.AddElement(imageElement);
            editParams.AddElement(textElement);
            editParams.AddElement(svgElement);

            editParams.OutputFileName = "editpdf-advanced";
            
            // Proces added files,
            // time var will contains information about time spent in process
            var time = task.Process(editParams);
            task.DownloadFile("path");
        }
    }
}