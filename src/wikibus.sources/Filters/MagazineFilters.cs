using Argolis.Hydra.Annotations;
using Argolis.Hydra.Resources;
using Argolis.Models;
using NullGuard;
using Vocab;

namespace Wikibus.Sources.Filters
{
    /// <summary>
    /// Defines filters of the magazines collection
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class MagazineFilters : ITemplateParameters<Collection<Magazine>>
    {
        [Variable("title")]
        [Property(DCTerms.title)]
        public string Title { get; set; }
    }
}