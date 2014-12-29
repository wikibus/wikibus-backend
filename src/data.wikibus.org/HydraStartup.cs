using System;
using Nancy.Bootstrapper;
using Nancy.Hydra;
using wikibus.common;

namespace data.wikibus.org
{
    /// <summary>
    /// Starts Hydra
    /// </summary>
    public class HydraStartup : IApplicationStartup
    {
        private readonly IWikibusConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="HydraStartup"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public HydraStartup(IWikibusConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Perform any initialization tasks
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            var baseUri = new Uri(_config.BaseApiNamespace);
            pipelines.UseHydra(new Uri(baseUri, "doc"));
        }
    }
}
