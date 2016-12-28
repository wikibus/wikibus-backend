using System;

namespace Argolis.Templates
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class CollectionIdentifierTemplateAttribute : TemplateAttributeBase
    {
        public CollectionIdentifierTemplateAttribute(string template)
        {
            this.Template = template;
        }

        public CollectionIdentifierTemplateAttribute(string template, Type filterType)
        {
            this.FilterType = filterType;
            this.Template = template;
        }

        public Type FilterType { get; set; }
    }
}