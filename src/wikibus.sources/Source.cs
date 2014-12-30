using System;
using Hydra.Annotations;
using NullGuard;
using wikibus.common.Vocabularies;

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
        [SupportedProperty(DCTerms.language)]
        public Language[] Languages
        {
            get { return _languages; }
            set { _languages = value; }
        }

        /// <summary>
        /// Gets or sets the pages count.
        /// </summary>
        [SupportedProperty(Bibo.pages)]
        public int? Pages { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication date date.
        /// </summary>
        [SupportedProperty(DCTerms.date)]
        public DateTime? Date { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication year.
        /// </summary>
        [SupportedProperty(Opus.year)]
        public int? Year { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication month.
        /// </summary>
        [SupportedProperty(Opus.month)]
        public int? Month { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [SupportedProperty(Schema.image)]
        public Image Image { [return: AllowNull] get; set; }
    }
}
