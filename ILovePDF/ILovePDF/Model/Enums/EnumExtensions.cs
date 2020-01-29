using System;
using System.ComponentModel;
using System.Reflection;

namespace LovePdf.Model.Enums
{
    /// <summary>
    ///     EnumExtensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetRuntimeField(value.ToString());

            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}