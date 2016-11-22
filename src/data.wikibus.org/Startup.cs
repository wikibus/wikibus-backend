using Data.Wikibus.Org;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Data.Wikibus.Org
{
    internal class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseNancy(options => options.Bootstrapper = new Bootstrapper());
            builder.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
