using ILovePDF;
using ILovePDF.Model.Enum;
using ILovePDF.Model.Enum.Params;
using ILovePDF.Model.Task;
using ILovePDF.Model.TaskParams;

namespace Samples
{
    public class CompressAdvanced
    {
        public void DoTask()
        {
            var api = new LovePdfApi("PUBLIC_KEY", "SECRET_KEY");

            var task = api.CreateTask<CompressTask>();

            //add file, and specify rotation
            var file = task.AddFile(path: "/path/to/document.pdf", rotate: Rotate._90);

            //set compress parameters and process files
            var time = task.Process(new CompressParams
            {
                CompressionLevel = CompressionLevels.extreme,
                OutputFileName = "extreme_compression"
            });

            //download output file(s) to specific directory
            task.DownloadFile("path/to/download");
        }
    }
}
