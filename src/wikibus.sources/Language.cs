using System.Globalization;
using Newtonsoft.Json;

namespace wikibus.sources
{
    /// <summary>
    /// Represents a language
    /// </summary>
    [JsonConverter(typeof(LexvoIso639LanguageConverter))]
    public class Language
    {
        private readonly CultureInfo _cultureInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        public Language(string name) : this(new CultureInfo(name))
        {
        }

        private Language(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
        }

        /// <summary>
        /// Gets the language's name
        /// </summary>
        public string Name
        {
            get { return _cultureInfo.Name; }
        }

        public static bool operator ==(Language left, Language right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Language left, Language right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Language)obj);
        }

        public override int GetHashCode()
        {
            return _cultureInfo.GetHashCode();
        }

        protected bool Equals(Language other)
        {
            return _cultureInfo.Equals(other._cultureInfo);
        }
    }
}
