using System.Globalization;
using Newtonsoft.Json;

namespace wikibus.sources
{
    /// <summary>
    /// Represents a language
    /// </summary>
    [JsonConverter(typeof(LexvoIso639LanguageConverter))]
    public class Language : CultureInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        public Language(string name) : base(name)
        {
        }
    }
}
