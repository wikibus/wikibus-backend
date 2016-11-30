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
        private readonly IBaseUriProvider baseUriProvider;
        private readonly ModelTemplateProvider modelTemplateProvider;

        public UriTemplateMatcher(IBaseUriProvider baseUriProvider)
        {
            this.baseUriProvider = baseUriProvider;
            this.modelTemplateProvider = new ModelTemplateProvider();
        }

        public UriTemplateMatches Match<T>(Uri uri)
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

            return new UriTemplateMatches(matches);
        }
    }
}