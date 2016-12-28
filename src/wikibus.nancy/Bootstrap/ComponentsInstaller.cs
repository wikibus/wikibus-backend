using System;
using Argolis.Models;
using JsonLD.Entities;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Routing;
using VDS.RDF.Query;
using Wikibus.Common;
using Wikibus.Sources;
using Wikibus.Sources.EF;
using ISourcesDatabaseSettings = Wikibus.Sources.DotNetRDF.ISourcesDatabaseSettings;

namespace Wikibus.Nancy
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

            this.Register(configuration);
            this.Register(new Lazy<ISparqlQueryProcessor>(() =>
            {
                var endpointUri = databaseSettings.SourcesSparqlEndpoint;
                return new RemoteQueryProcessor(new SparqlRemoteEndpoint(endpointUri));
            }));
            this.Register<IFrameProvider>(new WikibusModelFrames());
            this.Register<DefaultRouteResolver>();
            this.Register<IBaseUriProvider>(typeof(BaseUriProvider));
        }
    }
}
