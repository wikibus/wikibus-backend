using System;
using System.Collections.Generic;
using Hydra;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
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

        [UsedImplicitly]
        private static JToken Context
        {
            get
            {
                const string hydra = "http://www.w3.org/ns/hydra/context.jsonld";
                var wikibus = new JObject(
                    new JProperty("api", "http://wikibus.org/api#"),
                    new JProperty(Wbo.Prefix, Wbo.BaseUri),
                    new JProperty(Schema.Prefix, Schema.BaseUri),
                    new JProperty(DCTerms.Prefix, DCTerms.BaseUri),
                    new JProperty(Opus.Prefix, Opus.BaseUri),
                    new JProperty(Bibo.Prefix, Bibo.BaseUri));

                return new JArray(hydra, wikibus);
            }
        }
    }
}
