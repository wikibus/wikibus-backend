using System;

namespace Argolis.Templates
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class IdentifierTemplateAttribute : Attribute
    {
        public IdentifierTemplateAttribute(string template)
        {
            this.Template = template;
        }

        public string Template { get; private set; }
    }
}