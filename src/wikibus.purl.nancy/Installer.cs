using Nancy;
using Nancy.Bootstrapper;
using Wikibus.Common;

namespace Wikibus.Purl.Nancy
{
    public class Installer : Registrations
    {
        public Installer(ITypeCatalog typeCatalog)
            : base(typeCatalog)
        {
            IWikibusConfiguration configuration = new AppSettingsConfiguration();

            this.Register(configuration);
        }
    }
}