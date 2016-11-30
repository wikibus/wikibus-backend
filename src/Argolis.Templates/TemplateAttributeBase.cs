using System;

namespace Argolis.Templates
{
    public abstract class TemplateAttributeBase : Attribute
    {
        public string Template { get; protected set; }
    }
}