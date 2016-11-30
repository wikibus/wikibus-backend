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
    }
}