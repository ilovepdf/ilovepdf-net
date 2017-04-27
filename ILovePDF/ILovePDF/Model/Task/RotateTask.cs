using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILovePDF.Model.Task
{
    public class RotateTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.rotate.ToString();
        }

        public string Process(RotateParams parameters = null)
        {
            if (parameters == null)
                parameters = new RotateParams();

            return base.Process(parameters);
        }
    }
}
