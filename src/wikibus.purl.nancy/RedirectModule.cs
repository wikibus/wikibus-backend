using Nancy;
using wikibus.common;

namespace wikibus.purl.nancy
{
    /// <summary>
    /// Module, which handles redirects like purl.org
    /// </summary>
    public class RedirectModule : NancyModule
    {
        private readonly IWikibusConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectModule"/> class.
        /// </summary>
        public RedirectModule(IWikibusConfiguration config)
        {
            _config = config;

            Get["/{path*}"] = rqst => RedirectRdfRequest();
        }

        private object RedirectRdfRequest()
        {
            string documentLocation = string.Format(_config.BaseApiNamespace + "{0}", Request.Path.Trim('/'));
            return Response.AsRedirect(documentLocation);
        }
    }
}
