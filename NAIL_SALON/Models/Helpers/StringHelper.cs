using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models.Helpers
{
    internal class StringHelper
    {
        public static string RemoveDiacritics(string text)
        {
            if(string.IsNullOrEmpty(text)) return text;

            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach( var c in normalized)
            {
                if(CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            var result = sb.ToString().Normalize(NormalizationForm.FormC);
            
            result = result.Replace('đ', 'd').Replace('Đ', 'D');

            return result;
        }
    }
}
