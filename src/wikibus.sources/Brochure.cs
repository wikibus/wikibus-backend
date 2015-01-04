using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;
using NullGuard;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// A brochure about buses, trams, etc.
    /// </summary>
    public class Brochure : Source
    {
        private string _description;
        private string _code;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [SupportedProperty(DCTerms.title)]
        public string Title { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [SupportedProperty(Rdfs.comment)]
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
        [SupportedProperty(DCTerms.identifier)]
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

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected static new JObject Context
        {
            get
            {
                var context = Source.Context;
                context.Add("description".IsProperty(Rdfs.comment));
                return context;
            }
        }

        [UsedImplicitly]
        private string Type
        {
            get { return Wbo.Brochure; }
        }
    }
}
