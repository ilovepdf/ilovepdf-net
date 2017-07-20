using LovePdf.Core;
using LovePdf.Model.Enums;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;

namespace Samples
{
    public class CompressAdvanced
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            var task = api.CreateTask<CompressTask>();

            //add file, and specify rotation
            var file = task.AddFile("/path/to/document.pdf", task.TaskId, Rotate.Degrees90);

            //set compress parameters and process files
            var time = task.Process(new CompressParams
            {
                CompressionLevel = CompressionLevels.Extreme,
                OutputFileName = "extreme_compression"
            });

            //download output file(s) to specific directory
            task.DownloadFile("path/to/download");
        }
    }
}
