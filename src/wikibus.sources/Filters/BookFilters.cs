using NullGuard;

namespace Wikibus.Sources.Filters
{
    /// <summary>
    /// Defines filters of the book collection
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class BookFilters : SourceFilters
    {
        public string Title { get; set; }

        public string Author { get; set; }
    }
}