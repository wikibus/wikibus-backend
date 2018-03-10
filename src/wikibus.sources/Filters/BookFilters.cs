using Argolis.Hydra.Annotations;
using Argolis.Hydra.Resources;
using Argolis.Models;
using NullGuard;
using Vocab;

namespace Wikibus.Sources.Filters
{
    /// <summary>
    /// Defines filters of the book collection
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class BookFilters : SourceFilters, ITemplateParameters<Collection<Book>>
    {
        [Variable("title")]
        [Property(DCTerms.title)]
        public string Title { get; set; }

        [Variable("author")]
        [Property(Schema.author)]
        public string Author { get; set; }
    }
}