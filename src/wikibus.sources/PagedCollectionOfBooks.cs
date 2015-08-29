using System.Collections.Generic;
using Hydra;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// Paged collection of books
    /// </summary>
    public class PagedCollectionOfBooks : PagedCollection<Book>
    {
        /// <summary>
        /// Gets the type of paged collection and typed flavor.
        /// </summary>
        public new IEnumerable<string> Type
        {
            get
            {
                yield return Hydra.Hydra.PagedCollection;
                yield return Wbo.PagedCollectionOfBooks;
            }
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title
        {
            get { return "Books"; }
        }
    }
}
