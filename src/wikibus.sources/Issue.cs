using System;
using Hydra.Annotations;
using JsonLD.Entities;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// A magazine issue
    /// </summary>
    [Class("http://schema.org/PublicationIssue")]
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Issue : Source
    {
        /// <summary>
        /// Gets or sets the magazine Uri.
        /// </summary>
        [SupportedProperty("sch:isPartOf")]
        [AllowGet]
        public Uri Magazine { get; set; }

        /// <summary>
        /// Gets or sets the issue number.
        /// </summary>
        [SupportedProperty("sch:issueNumber")]
        public string Number { get; set; }
    }
}
