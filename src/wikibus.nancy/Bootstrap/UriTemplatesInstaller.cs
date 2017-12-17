using Argolis.Models;
using Argolis.Models.TunnelVisionLabs;
using Nancy;
using Nancy.Bootstrapper;

namespace Wikibus.Nancy
{
    public class UriTemplatesInstaller : Registrations
    {
        public UriTemplatesInstaller(ITypeCatalog typeCatalog)
            : base(typeCatalog)
        {
            this.Register<IUriTemplateExpander>(typeof(TunnelVisionLabsUriTemplateExpander));
            this.Register<IUriTemplateMatcher>(typeof(TunnelVisionLabsUriTemplateMatcher));
        }
    }
}