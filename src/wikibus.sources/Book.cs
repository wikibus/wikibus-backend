using JsonLD.Entities;

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
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }
}
