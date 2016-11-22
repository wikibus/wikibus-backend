using System.Collections.Generic;
using System.ComponentModel;
using Hydra.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;
using Wikibus.Common.Vocabularies;

namespace Wikibus.Sources
{
    /// <summary>
    /// A brochure about buses, trams, etc.
    /// </summary>
    [SupportedClass(Wbo.Brochure)]
    public class Brochure : Source
    {
        private string description;
        private string code;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [ReadOnly(true)]
        public string Title { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [ReadOnly(true)]
        public string Description
        {
            [return: AllowNull]
            get
            {
                return this.description;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                this.description = value;
            }
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [ReadOnly(true)]
        public string Code
        {
            [return: AllowNull]
            get
            {
                return this.code;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                this.code = value;
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
