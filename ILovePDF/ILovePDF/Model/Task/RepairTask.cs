
using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class RepairTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.repair.ToString();
        }

        public string Process(RepairParams paramaters = null)
        {
            if(paramaters == null)
                paramaters = new RepairParams();

            return base.Process(paramaters);
        }
    }
}
