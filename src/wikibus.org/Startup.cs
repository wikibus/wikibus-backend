using Microsoft.Owin;
using Owin;
using Wikibus.Org;

[assembly: OwinStartup(typeof(Startup))]

namespace Wikibus.Org
{
    internal class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseNancy();
        }
    }
}
