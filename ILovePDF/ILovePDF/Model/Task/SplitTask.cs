using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class SplitTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.split.ToString();
        }

        public string Process(SplitParams parameters)
        {
            return base.Process(parameters);
        }
    }

}
