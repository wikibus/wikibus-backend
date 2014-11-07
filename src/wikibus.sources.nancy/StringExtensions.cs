using System.Text.RegularExpressions;

namespace wikibus.sources.nancy
{
    internal static class StringExtensions
    {
        private static readonly Regex FirstSegmentRegex = new Regex(@"^\/?[^\/]*\/");

        public static string StripFirstSegment(this string path)
        {
            return FirstSegmentRegex.Replace(path, string.Empty);
        }
    }
}