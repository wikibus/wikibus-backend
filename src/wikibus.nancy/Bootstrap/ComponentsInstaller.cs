using System;
using JsonLD.Entities;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Routing;
using VDS.RDF.Query;
using wikibus.common;
using wikibus.sources;
using ISourcesDatabaseSettings = wikibus.sources.dotNetRDF.ISourcesDatabaseSettings;

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
        /// <param name="catalog">Nancy type catalog</param>
        public ComponentsInstaller(ISourcesDatabaseSettings databaseSettings, ITypeCatalog catalog)
            : base(catalog)
        {
            IWikibusConfiguration configuration = new AppSettingsConfiguration();

            Register(configuration);
            Register(new Lazy<ISparqlQueryProcessor>(() =>
            {
                var endpointUri = databaseSettings.SourcesSparqlEndpoint;
                return new RemoteQueryProcessor(new SparqlRemoteEndpoint(endpointUri));
            }));
            Register<IFrameProvider>(new WikibusModelFrames());
            Register<DefaultRouteResolver>();
        }
    }
}
