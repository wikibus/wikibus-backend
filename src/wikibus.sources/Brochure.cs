using Hydra.Annotations;
using JsonLD.Entities;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// A brochure about buses, trams, etc.
    /// </summary>
    [Class("wbo:Brochure")]
    public class Brochure : Source
    {
        private string _description;
        private string _code;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [SupportedProperty("dcterms:title")]
        public string Title { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [SupportedProperty("rdfs:comment")]
        public string Description
        {
            [return: AllowNull]
            get
            {
                return _description;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                _description = value;
            }
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [SupportedProperty("dcterms:identifier")]
        public string Code
        {
            [return: AllowNull]
            get
            {
                return _code;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                _code = value;
            }
        }
    }
}
