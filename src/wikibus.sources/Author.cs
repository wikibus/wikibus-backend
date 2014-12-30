using JsonLD.Entities;
using NullGuard;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// Book author
    /// </summary>
    [Class(Schema.Person)]
    public class Author
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { [return: AllowNull] get; set; }
    }
}
