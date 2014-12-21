using JsonLD.Entities;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// A periodical about public transport
    /// </summary>
    [Class("http://wikibus.org/ontology#Magazine")]
    [Class("http://schema.org/Periodical")]
    [NullGuard(ValidationFlags.ReturnValues)]
    public class Magazine
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }
}
