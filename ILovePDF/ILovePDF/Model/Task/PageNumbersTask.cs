using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class PageNumbersTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.pagenumber.ToString();
        }

        public string Process(PageNumbersParams parameters = null)
        {
            if(parameters == null)
                parameters = new PageNumbersParams();

            return base.Process(parameters);
        }
    }
}
