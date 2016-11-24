using NullGuard;

namespace Wikibus.Sources.Filters
{
    [NullGuard(ValidationFlags.None)]
    public class SourceFilters
    {
        public string Language { get; set; }
    }
}