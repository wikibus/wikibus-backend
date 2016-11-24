namespace Wikibus.Sources.Filters
{
    /// <summary>
    /// Defines filters of the magazines collection
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class MagazineFilters
    {
        public string Title { get; set; }
    }
}