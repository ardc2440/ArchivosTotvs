using System.Text.RegularExpressions;

namespace Totvs.FlatFileGenerator.Infrastructure.Common.Extensions
{
    public static class StringExtensions
    {
        public static string StripNewLines(this string str)
        {
            if (str == null)
                return "";
            return Regex.Replace(str, @"\r\n?|\n", " ");
        }
    }
}
