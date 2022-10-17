using LovePdf.Model.TaskParams.Edit;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LovePdf.Core;

namespace LovePdf.Helpers
{
    public static class InitialValueHelper
    {
        public static IEnumerable<KeyValuePair<string, string>> GetInitialValues(object element, string keyPrefix) 
        {
            //Serializing and deserializing to get properties from derived class, since those properties only available in runtime.
            var json = JsonConvert.SerializeObject(element, new KeyValuePairConverter());
            var paramArray = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return paramArray.Keys.Select(
                paramKey => new KeyValuePair<string, string>(
                    StringHelpers.Invariant($"{keyPrefix}[{paramKey}]"),
                    paramArray[paramKey]));
        }
    }
}
