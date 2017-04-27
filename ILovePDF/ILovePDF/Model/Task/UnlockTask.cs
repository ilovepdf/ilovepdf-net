using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class UnlockTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.unlock.ToString();
        }

        public string Process(UnlockParams parameters = null)
        {
            if (parameters == null)
                parameters = new UnlockParams();

            return base.Process(parameters);
        }
    }

}
