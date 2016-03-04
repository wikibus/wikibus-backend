using System;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;

namespace wikibus.nancy
{
    /// <summary>
    /// Bootstrapper for wikibus.org API app
    /// </summary>
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper()
        {
            AppDomainAssemblyTypeScanner.AddAssembliesToScan(
                "JsonLD.Entities",
                "wikibus.sources.dotNetRDF",
                "wikibus.common");
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
        /// Applications the startup.
        /// </summary>
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
#if DEBUG
            StaticConfiguration.DisableErrorTraces = false;
#endif
        }

        private static bool IsNotNancyProcessor(Type responseProcessor)
        {
            return responseProcessor.Assembly != typeof(INancyBootstrapper).Assembly || responseProcessor == typeof(ResponseProcessor);
        }
    }
}
