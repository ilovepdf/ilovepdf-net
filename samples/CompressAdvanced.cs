using System.Diagnostics.CodeAnalysis;
using iLovePdf.Core;
using iLovePdf.Model.Enums;
using iLovePdf.Model.Task;
using iLovePdf.Model.TaskParams;

namespace Samples
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class CompressAdvanced
    {
        public void DoTask()
        {
            var api = new iLovePdfApi("PUBLIC_KEY", "SECRET_KEY");

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