using System.Collections.Generic;
using Hydra.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// A brochure about buses, trams, etc.
    /// </summary>
    [SupportedClass(Wbo.Brochure)]
    public class Brochure : Source
    {
        private string _description;
        private string _code;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
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

        /// <summary>
        /// Gets the types.
        /// </summary>
        protected override IEnumerable<string> Types
        {
            get
            {
                ////foreach (var type in base.Types)
                ////{
                ////    yield return type;
                ////}

                yield return Wbo.Brochure;
            }
        }
    }
}
