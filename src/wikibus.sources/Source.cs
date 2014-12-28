using System;
using Hydra.Annotations;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// A bibliographical source of knowledge about public transport
    /// </summary>
    public class Source
    {
        private Language[] _languages = new Language[0];

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        [SupportedProperty("dcterms:language")]
        public Language[] Languages
        {
            get { return _languages; }
            set { _languages = value; }
        }

        /// <summary>
        /// Gets or sets the pages count.
        /// </summary>
        [SupportedProperty("bibo:pages")]
        public int? Pages { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication date date.
        /// </summary>
        [SupportedProperty("dcterms:date")]
        public DateTime? Date { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication  year.
        /// </summary>
        [SupportedProperty("opus:year")]
        public int? Year { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication month.
        /// </summary>
        [SupportedProperty("opus:month")]
        public int? Month { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [SupportedProperty("sch:image")]
        public Image Image { [return: AllowNull] get; set; }
    }
}
