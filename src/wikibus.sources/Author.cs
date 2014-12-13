using JsonLD.Entities;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// Book author
    /// </summary>
    [Class("http://schema.org/Person")]
    public class Author
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { [return: AllowNull] get; set; }
    }
}
