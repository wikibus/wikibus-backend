using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TunnelVisionLabs.Net;

namespace Argolis.Templates
{
    /// <summary>
    /// Simplifies accessing and transforming URI Templates for resources
    /// </summary>
    public static class IdentifierOf<T>
    {
        public static string Template => typeof(T).GetCustomAttribute<IdentifierTemplateAttribute>().Template;

        public static IdentifierMatches Match(Uri uri, string baseUri = null)
        {
            var matches = new Dictionary<string, object>();

            uri = new Uri(Uri.EscapeUriString(uri.ToString()));
            var templateMatch = GetTemplate(baseUri).Match(uri);

            if (templateMatch != null)
            {
                matches = templateMatch.Bindings.ToDictionary(m => m.Key, m => m.Value.Value);
            }

            return new IdentifierMatches(matches);
        }

        public static Uri Bind(Dictionary<string, object> dictionary, string baseUri = null)
        {
            return GetTemplate(baseUri).BindByName(dictionary);
        }

        private static UriTemplate GetTemplate(string baseUri = null)
        {
            return new UriTemplate(baseUri + Template);
        }

        public class IdentifierMatches
        {
            private readonly Dictionary<string, object> dictionary;

            public IdentifierMatches(Dictionary<string, object> dictionary)
            {
                this.dictionary = dictionary;
            }

            public TOut Get<TOut>(string key)
            {
                var valueType = Nullable.GetUnderlyingType(typeof(TOut));

                if (valueType == null)
                {
                    valueType = typeof(TOut);
                }

                if (this.dictionary.ContainsKey(key))
                {
                    return (TOut)Convert.ChangeType(this.dictionary[key], valueType);
                }

                return default(TOut);
            }
        }
    }
}