using System;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.DryIoc;
using Nancy.Diagnostics;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;

namespace wikibus.nancy
{
    /// <summary>
    /// Bootstrapper for wikibus.org API app
    /// </summary>
    public class Bootstrapper : DryIocNancyBootstrapper
    {
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

        private static bool IsNotNancyProcessor(Type responseProcessor)
        {
            return responseProcessor.Assembly != typeof(INancyBootstrapper).Assembly || responseProcessor == typeof(ResponseProcessor);
        }
    }
}
