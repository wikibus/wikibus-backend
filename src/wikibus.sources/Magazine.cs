using System;
using Hydra.Annotations;
using JsonLD.Entities;
using NullGuard;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// A periodical about public transport
    /// </summary>
    [Class("wbo:Magazine")]
    [Class(Schema.Periodical)]
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Magazine
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [SupportedProperty(DCTerms.title)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the issues Uri.
        /// </summary>
        [SupportedProperty("wbo:issues")]
        [AllowGet]
        public Uri Issues
        {
            get { return new Uri(Id + "/issues"); }
        }
    }
}
