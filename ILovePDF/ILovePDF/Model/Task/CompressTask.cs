using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class CompressTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.compress.ToString();
        }

        public string Process(CompressParams parameters = null)
        {
            if (parameters == null)
            {
                parameters = new CompressParams();
            }
            return base.Process(parameters);
        }
    }
}
