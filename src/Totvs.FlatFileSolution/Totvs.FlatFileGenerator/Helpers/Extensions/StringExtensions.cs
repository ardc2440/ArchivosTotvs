using System.Text.RegularExpressions;

namespace Totvs.FlatFileGenerator.Helpers.Extensions
{
    internal static class StringExtensions
    {
        public static string StripNewLines(this string str)
        {
            return Regex.Replace(str, @"\r\n?|\n", " ");
        }
    }
}
