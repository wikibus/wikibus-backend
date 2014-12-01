using JsonLD.Entities;

namespace wikibus.sources
{
    /// <summary>
    /// A brochure about buses, trams, etc.
    /// </summary>
    [Class("http://wikibus.org/ontology#Brochure")]
    public class Brochure : Source
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }
}
