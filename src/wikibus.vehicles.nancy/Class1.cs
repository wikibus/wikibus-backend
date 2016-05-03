using Nancy;
using Nancy.Security;

namespace wikibus.vehicles.nancy
{
    public class ManufacturerCommandModule : NancyModule
    {
        public ManufacturerCommandModule()
        {
            this.RequiresAuthentication();
        }
    }
}
