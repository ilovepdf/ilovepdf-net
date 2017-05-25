using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILovePDF.Model.Task
{
    public class ValidatepdfaTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.validatepdfa.ToString();
        }
        public string Process(ValidatepdfaTaskParams parameters)
        {
            return base.Process(parameters);
        }
    }
}
