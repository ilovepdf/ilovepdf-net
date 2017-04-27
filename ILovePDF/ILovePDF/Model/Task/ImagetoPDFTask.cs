using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;

namespace ILovePDF.Model.Task
{
    public class ImageToPDFTask : LovePdfTask
    {
        public string ToolName { get; set; }

        public override string GetToolName()
        {
            return TaskName.imagepdf.ToString();
        }

        public string Process(ImageToPdfParams parameters = null)
        {
            if (parameters == null)
                parameters = new ImageToPdfParams();

            return base.Process(parameters);
        }


    }
}
