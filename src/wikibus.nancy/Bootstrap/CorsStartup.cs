using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using wikibus.common;

namespace wikibus.nancy
{
    /// <summary>
    /// Attaches to Nancy pipeline to add CORS headers
    /// </summary>
    public class CorsStartup : IApplicationStartup
    {
        private const string AllowHeadersHeader = "Access-Control-Allow-Headers";
        private const string AllowOriginHeader = "Access-Control-Allow-Origin";
        private const string AllowMethodHeader = "Access-Control-Allow-Methods";
        private const string AllowHeader = "Allow";

        private readonly IWikibusConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorsStartup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CorsStartup(IWikibusConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Append CORS headers to reponses
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(AppendCorsHeaders);
        }

        private void AppendCorsHeaders(NancyContext context)
        {
            context.Response
             .WithHeader(AllowOriginHeader, context.Request.Headers["Origin"].FirstOrDefault() ?? "*")
             .WithHeader(AllowMethodHeader, "POST, GET, DELETE, PUT, OPTIONS")
             .WithHeader(AllowHeadersHeader, "Accept, Origin, Content-type, X-Requested-With")
             .WithHeader(AllowHeader, "POST, GET, DELETE, PUT, OPTIONS");
        }
    }
}
