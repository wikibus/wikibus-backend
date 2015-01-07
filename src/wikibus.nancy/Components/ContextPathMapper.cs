using System;
using Hydra;
using wikibus.common;
using wikibus.sources;

namespace wikibus.nancy
{
    /// <summary>
    /// Maps wikibus models to remote contexts
    /// </summary>
    public class ContextPathMapper : Nancy.RDF.Contexts.ContextPathMapper
    {
        private readonly IWikibusConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextPathMapper"/> class.
        /// </summary>
        public ContextPathMapper(IWikibusConfiguration config)
        {
            _config = config;

            ServeContextOf<Book>();
            ServeContextOf<Brochure>();
            ServeContextOf<Issue>();
            ServeContextOf<Magazine>();
            ServeContextOf<PagedCollection<Book>>("PagedCollectionOfBooks");
            ServeContextOf<PagedCollection<Brochure>>("PagedCollectionOfBrochures");
            ServeContextOf<PagedCollection<Issue>>("PagedCollectionOfIssues");
        }

        /// <summary>
        /// Gets the application path.
        /// </summary>
        public override Uri AppPath
        {
            get { return new Uri(_config.BaseResourceNamespace); }
        }

        /// <summary>
        /// Gets the base path at which @contexts will be served.
        /// </summary>
        public override string BasePath
        {
            get { return "context"; }
        }
    }
}
