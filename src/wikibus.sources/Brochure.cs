using JsonLD.Entities;
using NullGuard;

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
        public string Title { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { [return: AllowNull] get; set; }
    }
}
