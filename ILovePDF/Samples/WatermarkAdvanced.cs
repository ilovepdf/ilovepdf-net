using ILovePDF;
using ILovePDF.Model.Enum.Params;
using ILovePDF.Model.Task;
using ILovePDF.Model.TaskParams;


namespace Samples
{
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
            var parameters = new WatermarkParams(
                new WatermarkModeText("watermark text"))
            {
                Pages = "1-5,7",
                VerticalPosition = WatermarkVerticalPositions.top,
                HorizontalPosition = WatermarkHorizontalPositions.right,
                VerticalPositionAdjustment = 100,
                HorizontalPositionAdjustment = 100,
                FontFamily = "Arial",
                FontStyle = FontStyles.Italic,
                FontSize = 12,
                FontColor = "#ff0000",
                Transparency = 50,
                Layer = Layer.below,
                OutputFileName = "watermarked"
            };

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(parameters);
            task.DownloadFile("path");
        }
    }
}
