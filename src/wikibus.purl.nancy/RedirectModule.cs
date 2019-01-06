using System;
using System.Linq;
using Nancy;
using Nancy.Responses.Negotiation;
using Wikibus.Common;

namespace Wikibus.Purl.Nancy
{
    /// <summary>
    /// Module, which handles redirects like purl.org
    /// </summary>
    public sealed class RedirectModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectModule"/> class.
        /// </summary>
        public RedirectModule(IWikibusConfiguration config)
        {
            this.Get("/{path*}", rqst => this.RedirectRdfRequest(config.BaseApiNamespace));
            this.Get("/{path*}", rqst => this.RedirectRdfRequest(config.BaseWebNamespace), IsHtmlRequest);
            this.Get("/", rqst => this.RedirectRdfRequest(config.BaseApiNamespace));
            this.Get("/", rqst => this.RedirectRdfRequest(config.BaseWebNamespace), IsHtmlRequest);
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
                Path = baseUri.AbsolutePath + this.Request.Url.Path.TrimStart('/'),
                Query = this.Request.Url.Query.TrimStart('?')
            };

            if (uri.Port == 80 || uri.Port == 443)
            {
                var uriNoPort = uri.Uri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                return this.Response.AsRedirect(uriNoPort);
            }

            return this.Response.AsRedirect(uri.ToString());
        }
    }
}
