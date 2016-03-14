using System;
using Hydra.Core;
using Hydra.Nancy;
using wikibus.common;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Factory of wikibus API documentation
    /// </summary>
    public class ApiDocumentationFactory : IApiDocumentationFactory
    {
        private readonly IWikibusConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentationFactory"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ApiDocumentationFactory(IWikibusConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        public ApiDocumentation CreateApiDocumentation()
        {
            return new WikibusApi(new Uri(_configuration.BaseApiNamespace));
        }
    }
}