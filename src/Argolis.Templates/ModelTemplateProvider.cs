using System;
using System.Reflection;

namespace Argolis.Templates
{
    public class ModelTemplateProvider
    {
        public string GetTemplate(Type type)
        {
            return type.GetCustomAttribute<IdentifierTemplateAttribute>().Template;
        }
    }
}