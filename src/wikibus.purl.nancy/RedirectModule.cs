using System;
using System.Linq;
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
            Get["/"] = rqst => RedirectRdfRequest();
        }

        private object RedirectRdfRequest()
        {
            var baseUri = new Uri(_config.BaseApiNamespace);

            var uri = new UriBuilder(_config.BaseApiNamespace)
            {
                Path = baseUri.AbsolutePath + Request.Url.Path.TrimStart('/'),
                Query = Request.Url.Query.TrimStart('?')
            };

            if (uri.Port == 80)
            {
                var uriNoPort = uri.Uri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                return Response.AsRedirect(uriNoPort);
            }

            return Response.AsRedirect(uri.ToString());
        }
    }
}
