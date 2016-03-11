using System;
using data.wikibus.org;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace data.wikibus.org
{
    internal class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.Use(async (context, func) =>
            {
                Console.WriteLine(context.Request.Uri);
                await func();
            });
            builder.UseNancy();
            builder.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
