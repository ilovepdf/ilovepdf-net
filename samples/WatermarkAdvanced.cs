using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class WatermarkAdvanced
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            //create watermark task
            var task = api.CreateTask<WaterMarkTask>();

            //file variable contains server file name
            var file = task.AddFile("path/to/file/document.pdf");


            //parameters that will be posted
            var parameters = new WaterMarkParams(
                new WatermarkModeText("watermark text"))
            {
                Pages = "1-5,7",
                VerticalPosition = WaterMarkVerticalPositions.Top,
                HorizontalPosition = WaterMarkHorizontalPositions.Right,
                VerticalPositionAdjustment = 100,
                HorizontalPositionAdjustment = 100,
                FontFamily = "Arial",
                FontStyle = FontStyles.Italic,
                FontSize = 12,
                FontColor = "#ff0000",
                Transparency = 50,
                Layer = Layer.Below,
                OutputFileName = "watermarked"
            };

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(parameters);
            task.DownloadFile("path");
        }
    }
}