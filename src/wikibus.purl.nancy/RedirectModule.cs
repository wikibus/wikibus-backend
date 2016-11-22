using System;
using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;
using wikibus.common;

namespace wikibus.purl.nancy
{
    /// <summary>
    /// Module, which handles redirects like purl.org
    /// </summary>
    public class RedirectModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectModule"/> class.
        /// </summary>
        public RedirectModule(IWikibusConfiguration config)
        {
            Get("/{path*}", rqst => RedirectRdfRequest(config.BaseApiNamespace));
            Get("/{path*}", rqst => RedirectRdfRequest(config.BaseWebNamespace), IsHtmlRequest);
            Get("/", rqst => RedirectRdfRequest(config.BaseApiNamespace));
            Get("/", rqst => RedirectRdfRequest(config.BaseWebNamespace), IsHtmlRequest);
        }

        private static bool IsHtmlRequest(NancyContext context)
        {
            return context.Request.Headers.Accept.Any(h => new MediaRange("text/html").Matches(new MediaRange(h.Item1)));
        }

        private object RedirectRdfRequest(string baseUrl)
        {
            var baseUri = new Uri(baseUrl);

            var uri = new UriBuilder(baseUrl)
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
