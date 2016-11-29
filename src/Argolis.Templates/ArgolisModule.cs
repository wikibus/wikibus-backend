using System;
using System.Reflection;

namespace Argolis.Templates
{
    public class ArgolisModule : Nancy.Routing.UriTemplates.UriTemplateModule
    {
        public virtual void Get<T>(Func<dynamic, dynamic> action)
        {
            this.Get<dynamic>(GetModelTemplate(typeof(T)), action);
        }

        private static string GetModelTemplate(Type type)
        {
            return type.GetCustomAttribute<IdentifierTemplateAttribute>().Template;
        }
    }
}