using iLovePdf.Core;
using iLovePdf.Model.TaskParams;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Linq;

namespace iLovePdf.Helpers
{
    public static class InitialValueHelper
    {
        public static IEnumerable<KeyValuePair<string, string>> GetInitialValues(object element, string keyPrefix)
        {
            // Serializing and deserializing to get properties from derived class, since those properties only available in runtime.

            if (element is PDFOCRParams stringList)
            {
                return stringList.OCRLanguages.Select((item, index) =>
                {
                    var key = string.IsNullOrEmpty(keyPrefix) ?
                        StringHelpers.Invariant($"{index}") :
                        StringHelpers.Invariant($"{keyPrefix}[{index}]");

                    return new KeyValuePair<string, string>(key, item.ToString());
                });
            }
            else
            {
                var json = JsonConvert.SerializeObject(element, new KeyValuePairConverter());
                var paramArray = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                return paramArray.Keys.Select(
                    paramKey =>
                    {
                        var key = string.IsNullOrEmpty(keyPrefix) ?
                            StringHelpers.Invariant($"{paramKey}") :
                            StringHelpers.Invariant($"{keyPrefix}[{paramKey}]");

                        return new KeyValuePair<string, string>(key, paramArray[paramKey]);
                    });
            }
        }
    }
}
