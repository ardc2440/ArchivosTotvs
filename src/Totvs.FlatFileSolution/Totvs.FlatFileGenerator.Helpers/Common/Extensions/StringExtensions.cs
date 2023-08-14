using System.Text.RegularExpressions;

namespace Totvs.FlatFileGenerator.Infrastructure.Common.Extensions
{
    public static class StringExtensions
    {
        public static string StripNewLines(this string str)
        {
            return Regex.Replace(str, @"\r\n?|\n", " ");
        }
    }
}
