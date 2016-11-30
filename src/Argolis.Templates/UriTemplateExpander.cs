using System;
using System.Collections.Generic;
using TunnelVisionLabs.Net;

namespace Argolis.Templates
{
    public class UriTemplateExpander
    {
        private readonly IBaseUriProvider baseUriProvider;
        private readonly ModelTemplateProvider modelTemplateProvider;

        public UriTemplateExpander(IBaseUriProvider baseUriProvider, ModelTemplateProvider modelTemplateProvider)
        {
            this.baseUriProvider = baseUriProvider;
            this.modelTemplateProvider = modelTemplateProvider;
        }

        public Uri ExpandAbsoluteUri<T>(IDictionary<string, object> dictionary)
        {
            var template = this.modelTemplateProvider.GetTemplate(typeof(T));

            return new UriTemplate(this.baseUriProvider.BaseUri + template).BindByName(dictionary);
        }

        public Uri ExpandRelativeUri<T>(IDictionary<string, object> dictionary)
        {
            var template = this.modelTemplateProvider.GetTemplate(typeof(T));

            return new UriTemplate(template).BindByName(dictionary);
        }
    }
}