using System.Collections.Generic;
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

            // Create edit task params builder
            var builder = new EditParamBuilder();

            // Add elements to Editpdf task in order of drawing (important if elements overlap!)

            // Create ImageElement
            var imageElement = builder.AddImage(imageFile.ServerFileName);
            imageElement.Coordinates = new Coordinate(300, 600);
            imageElement.Pages = 3;
            imageElement.Opacity = 40;

            // Create TextElement
            var textElement = builder.AddText("This is a sample text");
            textElement.Coordinates = new Coordinate(300, 600);
            textElement.Pages = 2;
            textElement.Align = TextAligments.Center;
            textElement.FontFamily = FontFamilies.TimesNewRoman;
            textElement.FontColor = "#FB8B24"; // Orange
            textElement.FontStyle = TextFontStyles.Bold;

            // Create SvgElement
            var svgElement = builder.AddSvg(svgFile.ServerFileName);
            svgElement.Coordinates = new Coordinate(300, 300);

            // Create edit task params
            var editParams = new EditParams(builder);
             
            editParams.OutputFileName = "editpdf-advanced";
            
            // Proces added files,
            // time var will contains information about time spent in process
            var time = task.Process(editParams);
            task.DownloadFile("path");
        }
    }
}