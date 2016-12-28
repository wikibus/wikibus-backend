using System;
using System.Collections.Generic;
using System.Linq;
using TunnelVisionLabs.Net;

namespace Argolis.Templates
{
    public class UriTemplateExpander : IUriTemplateExpander
    {
        private readonly IModelTemplateProvider modelTemplateProvider;

        public UriTemplateExpander(IModelTemplateProvider modelTemplateProvider)
        {
            this.modelTemplateProvider = modelTemplateProvider;
        }

        public Uri ExpandAbsolute<T>(object templateVariables)
        {
            var type = templateVariables.GetType();
            var props = type.GetProperties();
            var dictionary = props.ToDictionary(p => p.Name, p => p.GetValue(templateVariables));

            return this.ExpandAbsolute<T>(dictionary);
        }

        public Uri ExpandAbsolute<T>(IDictionary<string, object> templateVariables)
        {
            var template = this.modelTemplateProvider.GetAbsoluteTemplate(typeof(T));

            return new UriTemplate(template).BindByName(templateVariables);
        }

        public Uri ExpandRelative<T>(object templateVariables)
        {
            var type = templateVariables.GetType();
            var props = type.GetProperties();
            var dictionary = props.ToDictionary(p => p.Name, p => p.GetValue(templateVariables));

            return this.ExpandRelative<T>(dictionary);
        }

        public Uri ExpandRelative<T>(IDictionary<string, object> templateVariables)
        {
            var template = this.modelTemplateProvider.GetTemplate(typeof(T));

            return new UriTemplate(template).BindByName(templateVariables);
        }
    }
}