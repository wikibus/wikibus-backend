using System;
using System.Linq;
using Autofac;
using Hydra;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Nancy.Diagnostics;
using Nancy.Responses.Negotiation;
using Nancy.Routing.UriTemplates;
using Wikibus.Common;
using Wikibus.Nancy.Hydra;
using Wikibus.Sources.DotNetRDF;
using Wikibus.Sources.EF;
using SourcesRepository = Wikibus.Sources.EF.SourcesRepository;

namespace Wikibus.Nancy
{
    /// <summary>
    /// Bootstrapper for wikibus.org API app
    /// </summary>
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        /// <summary>
        /// Gets overridden configuration
        /// </summary>
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(c =>
                {
                    c.ResponseProcessors = c.ResponseProcessors.Where(IsNotNancyProcessor).ToList();
                    c.RouteResolver = typeof(UriTemplateRouteResolver);
                });
            }
        }

        /// <summary>
        /// Configures the Nancy environment
        /// </summary>
        /// <param name="environment">The <see cref="T:Nancy.Configuration.INancyEnvironment" /> instance to configure</param>
        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);

            environment.Tracing(true, true);
            environment.Diagnostics("wb");
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
                builder.RegisterType<EntityFactory>().AsSelf();
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
