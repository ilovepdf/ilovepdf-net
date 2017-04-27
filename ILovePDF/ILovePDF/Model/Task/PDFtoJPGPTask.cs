using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class PDFtoJPGPTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.pdfjpg.ToString();
        }

        public string Execute(PDFtoJPGParams parameters = null)
        {
            if(parameters == null)
                parameters = new PDFtoJPGParams();

            return base.Process(parameters);
        }

    }
}
