using System;

namespace Argolis.Templates
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class IdentifierTemplateAttribute : TemplateAttributeBase
    {
        public IdentifierTemplateAttribute(string template)
        {
            this.Template = template;
        }
    }
}