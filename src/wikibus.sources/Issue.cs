using System.Collections.Generic;
using System.ComponentModel;
using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;
using Wikibus.Common.Vocabularies;

namespace Wikibus.Sources
{
    /// <summary>
    /// A magazine issue
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    [SupportedClass(Wbo.MagazineIssue)]
    public class Issue : Source
    {
        /// <summary>
        /// Gets or sets the magazine Uri.
        /// </summary>
        [ReadOnly(true)]
        [Range(Wbo.Magazine)]
        public Magazine Magazine { get; set; }

        /// <summary>
        /// Gets or sets the issue number.
        /// </summary>
        [ReadOnly(true)]
        public string Number { get; set; }

        /// <summary>
        /// Gets the types.
        /// </summary>
        protected override IEnumerable<string> Types
        {
            get
            {
                foreach (var type in base.Types)
                {
                    yield return type;
                }

                yield return Schema.PublicationIssue;
            }
        }

        [UsedImplicitly]
        private static new JToken Context
        {
            get
            {
                var context = new JObject
                {
                    "number".IsProperty(Schema.issueNumber),
                    "magazine".IsProperty(Schema.isPartOf)
                };
                return Source.Context.MergeWith(Magazine.Context, context);
            }
        }
    }
}
