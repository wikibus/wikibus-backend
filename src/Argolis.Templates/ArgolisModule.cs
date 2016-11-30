using System;

namespace Argolis.Templates
{
    /// <summary>
    /// A <see cref="Nancy.NancyModule"/> with routing which doesn't require paths.
    /// They are taken from model's <see cref="IdentifierTemplateAttribute"/>
    /// </summary>
    /// <seealso cref="Nancy.Routing.UriTemplates.UriTemplateModule" />
    public class ArgolisModule : Nancy.Routing.UriTemplates.UriTemplateModule
    {
        public virtual void Get<T>(Func<dynamic, dynamic> action)
        {
            this.Get<dynamic>(IdentifierOf<T>.Template, action);
        }
    }
}