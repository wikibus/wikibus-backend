using NullGuard;
using Vocab;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// Book author
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { [return: AllowNull] get; set; }

        private string Type
        {
            get { return Schema.Person; }
        }
    }
}
