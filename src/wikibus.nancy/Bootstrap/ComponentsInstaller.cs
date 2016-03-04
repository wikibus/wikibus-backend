using System;
using System.Configuration;
using System.IO;
using JsonLD.Entities;
using Nancy.Bootstrapper;
using VDS.RDF.Query;
using wikibus.common;
using wikibus.sources;
using wikibus.sources.dotNetRDF;

namespace wikibus.nancy
{
    /// <summary>
    /// Install all required components
    /// </summary>
    public class ComponentsInstaller : Registrations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentsInstaller"/> class.
        /// </summary>
        /// <param name="databaseSettings">Database configuration provider</param>
        public ComponentsInstaller(ISourcesDatabaseSettings databaseSettings)
        {
            IWikibusConfiguration configuration = new AppSettingsConfiguration();

            Register(configuration);
            Register(new Lazy<ISparqlQueryProcessor>(() =>
            {
                var endpointUri = databaseSettings.SourcesSparqlEndpoint;
                return new RemoteQueryProcessor(new SparqlRemoteEndpoint(endpointUri));
            }));
            Register(CreateFrameProvider());
        }

        private static IFrameProvider CreateFrameProvider()
        {
            var frameProvider = new StaticFrameProvider();
            frameProvider.SetupSourcesFrames();
            return frameProvider;
        }
    }
}
