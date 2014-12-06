using System;
using System.Globalization;
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
        public Uri Id { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        public CultureInfo[] Langauges { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the pages count.
        /// </summary>
        public int Pages { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication date date.
        /// </summary>
        public DateTime Date { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication  year.
        /// </summary>
        public int Year { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication month.
        /// </summary>
        public int Month { [return: AllowNull] get; set; }
    }
}
