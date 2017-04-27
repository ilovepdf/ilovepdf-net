using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class PDFATask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.pdfjpg.ToString();
        }

        public string Process(PDFtoPDFAParams parameters = null)
        {
            if (parameters == null)
                parameters = new PDFtoPDFAParams();

            return base.Process(parameters);
        }
    }
}
