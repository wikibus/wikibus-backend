using Microsoft.Owin;
using Owin;
using wikibus.org;

[assembly: OwinStartup(typeof(Startup))]

namespace wikibus.org
{
    internal class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseNancy();
        }
    }
}
