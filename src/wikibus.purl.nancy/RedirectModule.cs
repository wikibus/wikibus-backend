using Nancy;

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
        public RedirectModule()
        {
            Get["/{path*}"] = rqst => RedirectRdfRequest();
        }

        private object RedirectRdfRequest()
        {
            string documentLocation = string.Format("http://wikibus.org/data/{0}", Request.Path.Trim('/'));
            return Response.AsRedirect(documentLocation);
        }
    }
}
