using System;
using System.Collections.Generic;
using System.Linq;
using TunnelVisionLabs.Net;

namespace Argolis.Templates
{
    public class UriTemplateMatcher
    {
        private readonly IBaseUriProvider baseUriProvider;
        private readonly ModelTemplateProvider modelTemplateProvider;

        public UriTemplateMatcher(IBaseUriProvider baseUriProvider)
        {
            this.baseUriProvider = baseUriProvider;
            this.modelTemplateProvider = new ModelTemplateProvider();
        }

        public IdentifierMatches Match<T>(Uri uri)
        {
            var matches = new Dictionary<string, object>();
            var template = this.modelTemplateProvider.GetTemplate(typeof(T));

            uri = new Uri(Uri.EscapeUriString(uri.ToString()));
            if (uri.IsAbsoluteUri)
            {
                template = this.baseUriProvider.BaseUri + template;
            }

            var templateMatch = new UriTemplate(template).Match(uri);

            if (templateMatch != null)
            {
                matches = templateMatch.Bindings.ToDictionary(m => m.Key, m => m.Value.Value);
            }

            return new IdentifierMatches(matches);
        }

        public class IdentifierMatches
        {
            private readonly Dictionary<string, object> dictionary;

            public IdentifierMatches(Dictionary<string, object> dictionary)
            {
                this.dictionary = dictionary;
            }

            public bool AreEmpty => this.dictionary.Count == 0;

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