using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// A book about public transport
    /// </summary>
    [Class("http://wikibus.org/ontology#Book")]
    public class Book : Source
    {
        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        public string ISBN { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public Author Author { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { [return: AllowNull] get; set; }
    }
}
