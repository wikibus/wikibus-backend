using System;
using System.Collections.Generic;
using System.ComponentModel;
using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// A periodical about public transport
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    [SupportedClass(Wbo.Magazine)]
    public class Magazine
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [ReadOnly(true)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the issues Uri.
        /// </summary>
        [ReadOnly(true)]
        [Range(Hydra.Hydra.Collection)]
        public Uri Issues
        {
            get { return new Uri(Id + "/issues"); }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        [UsedImplicitly]
        internal static JObject Context
        {
            get
            {
                return new JObject(
                    "title".IsProperty(DCTerms.title),
                    "issues".IsProperty(Api.issues).Type().Id());
            }
        }

        [JsonProperty, UsedImplicitly]
        private IEnumerable<string> Types
        {
            get
            {
                yield return Wbo.Magazine;
                yield return Schema.Periodical;
            }
        }
    }
}
