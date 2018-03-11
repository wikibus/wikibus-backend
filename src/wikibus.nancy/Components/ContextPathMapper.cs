using Argolis.Hydra.Resources;
using Nancy.Rdf.Contexts;
using Wikibus.Sources;

namespace Wikibus.Nancy
{
    /// <summary>
    /// Maps wikibus models to remote contexts
    /// </summary>
    public class ContextPathMapper : DefaultContextPathMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextPathMapper"/> class.
        /// </summary>
        public ContextPathMapper()
        {
            this.ServeContextOf<Book>();
            this.ServeContextOf<Brochure>();
            this.ServeContextOf<Issue>();
            this.ServeContextOf<Magazine>();
            this.ServeContextOf<SearchableCollection<Book>>("CollectionOfBooks");
            this.ServeContextOf<SearchableCollection<Brochure>>("CollectionOfBrochures");
            this.ServeContextOf<SearchableCollection<Magazine>>("CollectionOfMagazines");
            this.ServeContextOf<Collection<Issue>>("CollectionOfIssues");
        }
    }
}
