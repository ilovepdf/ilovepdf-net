using System.Text.RegularExpressions;

namespace LovePdf.Helpers
{
    /// <summary>
    /// Validation Helper
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Checks the input hex color string for validity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateHexColor(string input)
        {
            return Regex.IsMatch(input, @"^#[a-fA-F0-9]{6}$");
        }

        /// <summary>
        /// Checks the input hex color string for validity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidColor(string input)
        {
            return input == "transparent" || ValidateHexColor(input);
        }
    }
}
