using LovePdf.Core;
using LovePdf.Model.Task;
using LovePdf.Model.TaskParams;

namespace ILovePDF_Nuget_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new LovePdfApi(@"project_public_3fedaeb8b6ff0e34a849c049422f4725_ZQHs-56404af1d4525a22201eeacc0b8e4ed0", @"secret_key_e6e42ebc47aaa75f7161b307a3464297__tMrk149492f181e7a12e0eae08527cfa166a");

            //create compress task
            var task = api.CreateTask<CompressTask>();


            //file variable contains server file name
            var file = task.AddFile(@"should-work.pdf");

            var taskParams = new CompressParams()
            {
                OutputFileName = @"test"
            };

            //proces added files
            //time var will contains information about time spent in process
            var time = task.Process(taskParams);

            //download files to specific folder
            task.DownloadFile();

        }
    }
}
