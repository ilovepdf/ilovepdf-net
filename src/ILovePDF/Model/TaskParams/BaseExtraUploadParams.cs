using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Model.TaskParams
{
    // This class is designed so that every programmer adapts it to the additional tools with specific needs and uses the SetValue method
    public abstract class BaseExtraUploadParams
    {
        protected Dictionary<string, string> extraParams = new Dictionary<string, string>();

        protected void SetValue(string key, string value)
        {
            extraParams[key] = value;
        }

        public Dictionary<string, string> GetValues()
        {
            return new Dictionary<string, string>(extraParams);
        }
    }
}
