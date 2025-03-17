using System;
using System.Collections.Generic;
using System.Text;

namespace iLovePdf.Helpers
{
    public class TextHelper
    {
        public static string RemoveAccents(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string[] accents = { "á", "é", "í", "ó", "ú", "ü", "ñ", "Á", "É", "Í", "Ó", "Ú", "Ü", "Ñ" };
            string[] withoutAccents = { "a", "e", "i", "o", "u", "u", "n", "A", "E", "I", "O", "U", "U", "N" };

            StringBuilder sb = new StringBuilder(input);

            for (int i = 0; i < accents.Length; i++)
            {
                sb.Replace(accents[i], withoutAccents[i]);
            }

            return sb.ToString();
        }
    }
}
