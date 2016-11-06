using System;
using System.Linq;
using Autofac;
using Hydra;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Diagnostics;
using Nancy.Responses.Negotiation;
using wikibus.common;
using wikibus.nancy.Hydra;
using wikibus.sources;
using wikibus.sources.dotNetRDF;
using wikibus.sources.EF;
using SourcesRepository = wikibus.sources.EF.SourcesRepository;

namespace wikibus.nancy
{
    /// <summary>
    /// Bootstrapper for wikibus.org API app
    /// </summary>
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper()
        {
            StaticConfiguration.DisableErrorTraces = false;
        }

        /// <summary>
        /// Gets overridden configuration
        /// </summary>
        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(c =>
                {
                    c.ResponseProcessors = c.ResponseProcessors.Where(IsNotNancyProcessor).ToList();
                });
            }
        }

        /// <summary>
        /// Gets the diagnostics configuration.
        /// </summary>
        /// <value>
        /// The diagnostics configuration.
        /// </value>
        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration { Password = @"wb" }; }
        }

        /// <summary>
        /// Configures the application container.
        /// </summary>
        /// <param name="existingContainer">The existing container.</param>
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            existingContainer.Update(builder =>
            {
                builder.RegisterType<AppSettingsConfiguration>().As<IWikibusConfiguration>();
                builder.RegisterType<HydraDocumentationSettings>().As<IHydraDocumentationSettings>();
                builder.RegisterType<ImageResizer>().As<IImageResizer>();
                builder.RegisterAssemblyTypes(typeof(SourcesRepository).Assembly)
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces();
                builder.RegisterType<SourceImagesRepository>().AsImplementedInterfaces();
                builder.RegisterType<IdRetriever>().AsSelf();
                builder.RegisterType<EntityFactory>().AsSelf();
                builder.RegisterType<IdentifierTemplates>().AsSelf();
            });

            base.ConfigureApplicationContainer(existingContainer);
        }

        /// <summary>
        /// Configures the request container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="context">The context.</param>
        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            container.Update(builder =>
            {
                builder.RegisterType<SourceContext>().AsImplementedInterfaces();
            });

            base.ConfigureRequestContainer(container, context);
        }

        private static bool IsNotNancyProcessor(Type responseProcessor)
        {
            return responseProcessor.Assembly != typeof(INancyBootstrapper).Assembly || responseProcessor == typeof(ResponseProcessor);
        }
    }
}
