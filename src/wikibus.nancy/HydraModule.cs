using System;
using Hydra;
using wikibus.common;

namespace wikibus.nancy
{
    /// <summary>
    /// Serves wikibus API
    /// </summary>
    public class HydraModule : Nancy.Hydra.HydraModule
    {
        private readonly IWikibusConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="HydraModule"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public HydraModule(IWikibusConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        protected override ApiDocumentation CreateApiDocumentation()
        {
            return new WikibusApi(new Uri(_config.BaseApiNamespace));
        }
    }
}
