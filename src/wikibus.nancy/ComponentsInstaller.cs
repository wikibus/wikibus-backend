using System.Configuration;
using System.IO;
using JsonLD.Entities;
using Nancy;
using Nancy.Bootstrapper;
using TCode.r2rml4net;
using VDS.RDF.Configuration;
using VDS.RDF.Query;
using wikibus.sources;
using wikibus.sources.dotNetRDF;

namespace wikibus.nancy
{
    /// <summary>
    /// Install all required components
    /// </summary>
    public class ComponentsInstaller : Registrations
    {
        private static readonly string DotNetRDFConfiguration = ConfigurationManager.AppSettings["dotnetrdf-config"];
        private static readonly string QueryProcessorName = ConfigurationManager.AppSettings["queryPorcessor"];

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentsInstaller"/> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public ComponentsInstaller(IRootPathProvider pathProvider)
        {
            var path = Path.Combine(pathProvider.GetRootPath(), DotNetRDFConfiguration);
            var configurationLoader = new ConfigurationLoader(path);
            ConfigurationLoader.AddObjectFactory(new StoreLoader());

            Register(configurationLoader.LoadObject<ISparqlQueryProcessor>(QueryProcessorName));
            Register(CreateContextProvider());
            Register(CreateFrameProvider());
            Register<IR2RML>(typeof(WikibusR2RML));
        }

        private static IFrameProvider CreateFrameProvider()
        {
            var frameProvider = new StaticFrameProvider();
            frameProvider.SetupSourcesFrames();
            return frameProvider;
        }

        private static IContextProvider CreateContextProvider()
        {
            var contextProvider = new StaticContextProvider();
            contextProvider.SetupSourcesContexts();
            return contextProvider;
        }
    }
}
