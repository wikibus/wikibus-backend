using System;
using Hydra.Annotations;
using JsonLD.Entities;
using NullGuard;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// A magazine issue
    /// </summary>
    [Class(Schema.PublicationIssue)]
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Issue : Source
    {
        /// <summary>
        /// Gets or sets the magazine Uri.
        /// </summary>
        [SupportedProperty(Schema.isPartOf)]
        [AllowGet]
        public Uri Magazine { get; set; }

        /// <summary>
        /// Gets or sets the issue number.
        /// </summary>
        [SupportedProperty(Schema.issueNumber)]
        public string Number { get; set; }
    }
}
