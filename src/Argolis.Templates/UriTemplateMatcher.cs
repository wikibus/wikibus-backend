using System;
using System.Collections.Generic;
using System.Linq;
using TunnelVisionLabs.Net;

namespace Argolis.Templates
{
    public interface IUriTemplateMatcher
    {
        UriTemplateMatches Match<T>(Uri uri);
    }

    public class UriTemplateMatcher : IUriTemplateMatcher
    {
        private readonly IModelTemplateProvider modelTemplateProvider;

        public UriTemplateMatcher(IModelTemplateProvider modelTemplateProvider)
        {
            this.modelTemplateProvider = modelTemplateProvider;
        }

        public UriTemplateMatches Match<T>(Uri uri)
        {
            var matches = new Dictionary<string, object>();

            uri = new Uri(Uri.EscapeUriString(uri.ToString()));
            var template = uri.IsAbsoluteUri
                ? this.modelTemplateProvider.GetAbsoluteTemplate(typeof(T))
                : this.modelTemplateProvider.GetTemplate(typeof(T));

            var templateMatch = new UriTemplate(template).Match(uri);

            if (templateMatch != null)
            {
                matches = templateMatch.Bindings.ToDictionary(m => m.Key, m => m.Value.Value);
            }

            return new UriTemplateMatches(matches);
        }
    }
}