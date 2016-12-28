using System;
using System.Collections.Generic;
using System.ComponentModel;
using Argolis.Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;
using Wikibus.Common;
using Wikibus.Common.JsonLd;

namespace Wikibus.Sources
{
    /// <summary>
    /// A bibliographical source of knowledge about public transport
    /// </summary>
    [SupportedClass(Wbo.Source)]
    public class Source
    {
        private Language[] languages = new Language[0];

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        [ReadOnly(true)]
        [Range(Xsd.gMonth)]
        public Language[] Languages
        {
            get { return this.languages; }
            set { this.languages = value; }
        }

        /// <summary>
        /// Gets or sets the pages count.
        /// </summary>
        [ReadOnly(true)]
        [Range(Xsd.nonNegativeInteger)]
        public int? Pages { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication date date.
        /// </summary>
        [ReadOnly(true)]
        [Range(Xsd.date)]
        public DateTime? Date { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication year.
        /// </summary>
        [ReadOnly(true)]
        [Range(Xsd.gYear)]
        public int? Year { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the publication month.
        /// </summary>
        [ReadOnly(true)]
        [Range(Xsd.gMonth)]
        public int? Month { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets the image.
        /// </summary>
        [ReadOnly(true)]
        [Range(Schema.ImageObject)]
        public Image Image { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets the @context.
        /// </summary>
        [UsedImplicitly]
        protected static JObject Context
        {
            get
            {
                return new JObject(
                    Prefix.Of(typeof(Bibo)),
                    Prefix.Of(typeof(DCTerms)),
                    Prefix.Of(typeof(Xsd)),
                    Prefix.Of(typeof(Opus)),
                    Prefix.Of(typeof(Rdfs)),
                    Prefix.Of(typeof(Schema)),
                    Prefix.Of(typeof(Wbo)),
                    "langIso".IsPrefixOf(Lexvo.iso639_1),
                    "year".IsProperty(Opus.year).Type().Is(Xsd.gYear),
                    "month".IsProperty(Opus.month).Type().Is(Xsd.gMonth),
                    "date".IsProperty(DCTerms.date).Type().Is(Xsd.date),
                    "pages".IsProperty(Bibo.pages).Type().Is(Xsd.integer),
                    "title".IsProperty(DCTerms.title),
                    "code".IsProperty(DCTerms.identifier),
                    "languages".IsProperty(DCTerms.language).Type().Id().Container().Set(),
                    "name".IsProperty(Schema.name),
                    "image".IsProperty(Schema.image),
                    "hasImage".IsProperty(Wbo.BaseUri + "hasImage"),
                    "contentUrl".IsProperty(Schema.contentUrl).Type().Is(Schema.URL),
                    "thumbnail".IsProperty(Schema.thumbnail));
            }
        }

        /// <summary>
        /// Gets the types.
        /// </summary>
        [JsonProperty]
        protected virtual IEnumerable<string> Types
        {
            get { yield return Wbo.Source; }
        }
    }
}
