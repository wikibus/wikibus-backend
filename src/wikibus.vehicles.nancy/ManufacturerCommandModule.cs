using Nancy;
using Nancy.Security;

namespace Wikibus.Vehicles.Nancy
{
    public class ManufacturerCommandModule : NancyModule
    {
        public ManufacturerCommandModule()
        {
            this.RequiresAuthentication();
        }
    }
}
