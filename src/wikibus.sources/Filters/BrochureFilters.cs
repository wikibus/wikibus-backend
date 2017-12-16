using Argolis.Hydra.Annotations;
using Argolis.Hydra.Resources;
using Argolis.Models;
using NullGuard;
using Vocab;

namespace Wikibus.Sources.Filters
{
    /// <summary>
    /// Defines filters of the brochures collection
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class BrochureFilters : SourceFilters, ITemplateParameters<Collection<Brochure>>
    {
        [Variable("title")]
        [Property(DCTerms.title)]
        public string Title { get; set; }
    }
}