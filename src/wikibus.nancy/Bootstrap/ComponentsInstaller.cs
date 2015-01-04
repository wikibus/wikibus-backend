using System.Configuration;
using System.IO;
using JsonLD.Entities;
using Nancy;
using Nancy.Bootstrapper;
using VDS.RDF.Configuration;
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
        private static readonly string DotNetRDFConfiguration = ConfigurationManager.AppSettings["dotnetrdf-config"];
        private static readonly string QueryProcessorName = ConfigurationManager.AppSettings["queryPorcessor"];

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentsInstaller"/> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public ComponentsInstaller(IRootPathProvider pathProvider)
        {
            var configPath = Path.Combine(pathProvider.GetRootPath(), DotNetRDFConfiguration);
            var storePath = Path.Combine(pathProvider.GetRootPath(), @"App_Data\sources.trig");
            var configurationLoader = new ConfigurationLoader(configPath);
            IWikibusConfiguration configuration = new AppSettingsConfiguration();
            ConfigurationLoader.AddObjectFactory(new StoreLoader(storePath, configuration));

            Register(configuration);
            Register(configurationLoader.LoadObject<ISparqlQueryProcessor>(QueryProcessorName));
            Register(CreateFrameProvider());
            Register<ISourceImagesRepository>(new SourceImagesRepository(ConfigurationManager.ConnectionStrings["sql"].ConnectionString));
            Register<IImageResizer>(new ImageResizer());
        }

        private static IFrameProvider CreateFrameProvider()
        {
            var frameProvider = new StaticFrameProvider();
            frameProvider.SetupSourcesFrames();
            return frameProvider;
        }
    }
}
