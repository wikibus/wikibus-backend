using System;
using System.Collections.Generic;
using Hydra;
using Newtonsoft.Json.Linq;
using wikibus.common.JsonLd;
using wikibus.common.Vocabularies;
using wikibus.sources;

namespace wikibus.nancy
{
    /// <summary>
    /// The wikibus.org API Documentation
    /// </summary>
    internal class WikibusApi : ApiDocumentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WikibusApi"/> class.
        /// </summary>
        public WikibusApi(Uri entryPoint)
            : base(entryPoint)
        {
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id
        {
            get { return new Uri(Entrypoint, "doc").ToString(); }
        }

        /// <summary>
        /// Gets the supported classes.
        /// </summary>
        public override IEnumerable<Class> SupportedClasses
        {
            get
            {
                return new[]
                {
                    typeof(EntryPoint).ToClass(),
                    typeof(Book).ToClass(),
                    typeof(Brochure).ToClass(),
                    typeof(Magazine).ToClass(),
                    typeof(Issue).ToClass()
                };
            }
        }

        /// <summary>
        /// Gets the @context for wikibus API.
        /// </summary>
        protected override JToken GetLocalContext()
        {
            return new JObject(
                Prefix.Of(typeof(Api)),
                Prefix.Of(typeof(Wbo)),
                Prefix.Of(typeof(Schema)),
                Prefix.Of(typeof(DCTerms)),
                Prefix.Of(typeof(Opus)),
                Prefix.Of(typeof(Bibo)));
        }
    }
}
