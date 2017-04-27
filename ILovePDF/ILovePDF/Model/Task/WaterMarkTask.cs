using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class WaterMarkTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.watermark.ToString();
        }

        public string Process(WatermarkParams parameters)
        {
            return base.Process(parameters);
        }
    }
}
