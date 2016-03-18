using System;
using Hydra;
using Hydra.Resources;
using wikibus.sources;

namespace wikibus.nancy
{
    /// <summary>
    /// Maps wikibus models to remote contexts
    /// </summary>
    public class ContextPathMapper : Nancy.Rdf.Contexts.DefaultContextPathMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextPathMapper"/> class.
        /// </summary>
        public ContextPathMapper()
        {
            ServeContextOf<Book>();
            ServeContextOf<Brochure>();
            ServeContextOf<Issue>();
            ServeContextOf<Magazine>();
            ServeContextOf<Collection<Book>>("CollectionOfBooks");
            ServeContextOf<Collection<Brochure>>("CollectionOfBrochures");
            ServeContextOf<Collection<Magazine>>("CollectionOfMagazines");
            ServeContextOf<Collection<Issue>>("CollectionOfIssues");
        }
    }
}
