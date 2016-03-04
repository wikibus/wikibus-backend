using System;
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
        private const string SourceTrigStore = @"App_Data\sources.trig";
        private const string SqlConnectionStringName = "sql";
        private static readonly string DotNetRDFConfiguration = ConfigurationManager.AppSettings["dotnetrdf-config"];
        private static readonly string QueryProcessorName = ConfigurationManager.AppSettings["queryPorcessor"];

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentsInstaller"/> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public ComponentsInstaller(IRootPathProvider pathProvider)
        {
            IWikibusConfiguration configuration = new AppSettingsConfiguration();

            Register(configuration);
            Register(new Lazy<ISparqlQueryProcessor>(() =>
            {
                var storePath = Path.Combine(pathProvider.GetRootPath(), SourceTrigStore);
                ConfigurationLoader.AddObjectFactory(new StoreLoader(storePath, configuration));
                var configPath = Path.Combine(pathProvider.GetRootPath(), DotNetRDFConfiguration);
                var configurationLoader = new ConfigurationLoader(configPath);

                return configurationLoader.LoadObject<ISparqlQueryProcessor>(QueryProcessorName);
            }));
            Register(CreateFrameProvider());
            Register<ISourceImagesRepository>(new SourceImagesRepository(ConfigurationManager.ConnectionStrings[SqlConnectionStringName].ConnectionString));
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
