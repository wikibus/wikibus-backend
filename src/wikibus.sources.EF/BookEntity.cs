using NullGuard;

namespace wikibus.sources.EF
{
    [NullGuard(ValidationFlags.None)]
    public class BookEntity : SourceEntity
    {
        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public string BookISBN { get; set; }
    }
}