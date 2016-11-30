using System;

namespace Argolis.Templates
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CollectionIdentifierTemplateAttribute : Attribute
    {
        public CollectionIdentifierTemplateAttribute(string template)
        {
            this.Template = template;
        }

        public string Template { get; private set; }
    }
}