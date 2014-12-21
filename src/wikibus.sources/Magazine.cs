using System;
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
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the issues Uri.
        /// </summary>
        public Uri Issues
        {
            get { return new Uri(Id + "/issues"); }
        }
    }
}
