using NullGuard;

namespace Wikibus.Sources.Filters
{
    /// <summary>
    /// Defines filters of the brochures collection
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class BrochureFilters
    {
        public string Title { get; set; }
    }
}