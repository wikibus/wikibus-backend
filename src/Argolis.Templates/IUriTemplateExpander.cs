using System;
using System.Collections.Generic;

namespace Argolis.Templates
{
    public interface IUriTemplateExpander
    {
        Uri ExpandAbsolute<T>(object templateVariables);

        Uri ExpandAbsolute<T>(IDictionary<string, object> templateVariables);

        Uri ExpandRelative<T>(object templateVariables);

        Uri ExpandRelative<T>(IDictionary<string, object> templateVariables);
    }
}