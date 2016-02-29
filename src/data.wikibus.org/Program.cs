using System;
using JsonLD.Entities;
using Microsoft.Owin.Hosting;
using Owin;
using wikibus.sources.dotNetRDF;

namespace data.wikibus.org
{
    internal class Program
    {
        private const int DefaultPort = 17899;

        private static void Main(string[] args)
        {
            int port = DefaultPort;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out port);
            }

            var urlBuilder = new UriBuilder("http://localhost")
            {
                Port = port
            };

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
