using Nancy;
using Nancy.Bootstrapper;

namespace Argolis.Templates
{
    public class Installer : Registrations
    {
        public Installer(ITypeCatalog typeCatalog)
            : base(typeCatalog)
        {
            this.RegisterWithDefault<IModelTemplateProvider>(typeof(ModelTemplateProvider));
        }
    }
}
