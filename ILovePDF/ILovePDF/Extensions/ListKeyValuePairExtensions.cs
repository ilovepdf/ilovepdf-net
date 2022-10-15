using LovePdf.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LovePdf.Extensions
{
    static class ListKeyValuePairExtensions
    {
        public static void AddItem(this List<KeyValuePair<string, string>> keyValuePairs, string paramKey, string value) 
        {
            keyValuePairs.Add(new KeyValuePair<string, string>(StringHelpers.Invariant(paramKey), value));
        }
    }
}
