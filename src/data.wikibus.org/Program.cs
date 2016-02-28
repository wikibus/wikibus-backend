using System;
using Microsoft.Owin.Hosting;
using Owin;
using wikibus.sources.dotNetRDF;

namespace data.wikibus.org
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var urlBuilder = new UriBuilder("http://localhost") {Port = 17899};

            //new JsonLD.Entities.EntitySerializer();
            //var x = typeof(SourcesRepository);

            using (WebApp.Start<Startup>(urlBuilder.ToString()))
            {
                Console.WriteLine("started nancy");
                Console.ReadLine();
            }
        }
    }

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
        }
    }
}
