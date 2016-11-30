using System;
using System.Collections.Generic;
using System.ComponentModel;
using Argolis.Templates;
using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;
using Wikibus.Common;

namespace Wikibus.Sources
{
    /// <summary>
    /// A periodical about public transport
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    [SupportedClass(Wbo.Magazine)]
    [IdentifierTemplate("magazine/{name}")]
    [CollectionIdentifierTemplate("magazines{?page,title}")]
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
            get { return new Uri(this.Id + "/issues"); }
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
