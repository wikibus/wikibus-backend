using System;
using JsonLD.Entities;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// A magazine issue
    /// </summary>
    [Class("http://schema.org/PublicationIssue")]
    [NullGuard(ValidationFlags.ReturnValues)]
    public class Issue : Source
    {
        /// <summary>
        /// Gets or sets the magazine Uri.
        /// </summary>
        public Uri Magazine { get; set; }

        /// <summary>
        /// Gets or sets the issue number.
        /// </summary>
        public string Number { get; set; }
    }
}
