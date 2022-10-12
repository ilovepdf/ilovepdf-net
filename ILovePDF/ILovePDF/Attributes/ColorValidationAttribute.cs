using LovePdf.Helpers;
using System.ComponentModel.DataAnnotations;

namespace LovePdf.Attributes
{
    public class ColorValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string strValue = value as string;

            if (value == null) { return false; }
            if (!string.IsNullOrEmpty(strValue))
            {
                return ValidationHelper.IsValidColor(strValue);
            }
            return true;

        }
    }
}
