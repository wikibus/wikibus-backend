using System.Collections.Generic;
using Hydra;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// Paged collection of brochures
    /// </summary>
    public class PagedCollectionOfBrochures : PagedCollection<Brochure>
    {
        /// <summary>
        /// Gets the type of paged collection and typed flavor.
        /// </summary>
        public new IEnumerable<string> Type
        {
            get
            {
                yield return Hydra.Hydra.PagedCollection;
                yield return Wbo.PagedCollectionOfBrochures;
            }
        }
    }
}
