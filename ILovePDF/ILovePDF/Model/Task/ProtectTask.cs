using ILovePDF.Model.Enum;
using ILovePDF.Model.TaskParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILovePDF.Model.Task
{
    public class ProtectTask : LovePdfTask
    {
        public override string GetToolName()
        {
            return TaskName.protect.ToString();
        }

        public string Process(ProtectParams parameters)
        {
            return base.Process(parameters);
        }

    }
}
