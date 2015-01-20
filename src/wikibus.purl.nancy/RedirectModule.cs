using System;
using System.Collections.Generic;
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

            var pathAndQuery = Request.Url.Path.Split('?');
            var path = pathAndQuery.ElementAtOrDefault(0);
            var query = pathAndQuery.ElementAtOrDefault(1);
            var uri = new UriBuilder(_config.BaseApiNamespace)
            {
                Path = baseUri.AbsolutePath + (path ?? string.Empty).TrimStart('/'),
                Query = query ?? string.Empty
            };

            var uriNoPort = uri.Uri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.SafeUnescaped);
            return Response.AsRedirect(uriNoPort);
        }
    }
}
