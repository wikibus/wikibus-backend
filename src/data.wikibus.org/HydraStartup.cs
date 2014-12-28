using System;
using System.Configuration;
using Nancy.Bootstrapper;
using Nancy.Hydra;

namespace data.wikibus.org
{
    /// <summary>
    /// Starts Hydra
    /// </summary>
    public class HydraStartup : IApplicationStartup
    {
        /// <summary>
        /// Perform any initialization tasks
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            var baseUri = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
            pipelines.UseHydra(new Uri(baseUri, "doc"));
        }
    }
}
