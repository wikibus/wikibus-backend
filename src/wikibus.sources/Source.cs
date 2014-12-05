using System;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// A bibliographical source of knowledge about public transport
    /// </summary>
    public class Source
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [AllowNull]
        public Uri Id { [return: AllowNull] get; set; }
    }
}
