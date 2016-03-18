using System.Collections.Generic;
using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;
using wikibus.common.Vocabularies;

namespace wikibus.sources
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
        [SupportedProperty(Schema.isPartOf)]
        [AllowGet]
        public Magazine Magazine { get; set; }

        /// <summary>
        /// Gets or sets the issue number.
        /// </summary>
        [SupportedProperty(Schema.issueNumber)]
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
        private static new JObject Context
        {
            get
            {
                var context = Source.Context;
                context.Add("number".IsProperty(Schema.issueNumber));
                context.Add("magazine".IsProperty(Schema.isPartOf));
                return context;
            }
        }
    }
}
