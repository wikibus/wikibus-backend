using System.Text.RegularExpressions;

namespace Wikibus.Sources.Nancy
{
    /// <summary>
    /// Extensions for manipulating string
    /// </summary>
    internal static class StringExtensions
    {
        private static readonly Regex FirstSegmentRegex = new Regex(@"^\/?[^\/]*\/");

        /// <summary>
        /// Attempts to strip the first URI segment.
        /// </summary>
        public static string StripFirstSegment(this string path)
        {
            return FirstSegmentRegex.Replace(path, string.Empty);
        }
    }
}
