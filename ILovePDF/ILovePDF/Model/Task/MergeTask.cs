using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class MergeTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.merge.ToString();
        }

        public string Process(MergeParams parameters)
        {
            if (parameters == null)
                parameters = new MergeParams();
            return base.Process(parameters);
        }
    }

    public class MergeParams : BaseParams { }
}
