﻿using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wikibus.common.JsonLd;
using wikibus.common.Vocabularies;

namespace wikibus.nancy
{
    /// <summary>
    /// The API entry point
    /// </summary>
    [SupportedClass(Api.EntryPoint)]
    public sealed class EntryPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPoint"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public EntryPoint(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the brochures Uri.
        /// </summary>
        [SupportedProperty(Api.brochures, Range = global::Hydra.Hydra.Collection)]
        [AllowGet(Range = global::Hydra.Hydra.Collection)]
        public string Brochures
        {
            get { return "brochures"; }
        }

        /// <summary>
        /// Gets the books Uri.
        /// </summary>
        [SupportedProperty(Api.books, Range = global::Hydra.Hydra.Collection)]
        [AllowGet(Range = global::Hydra.Hydra.Collection)]
        public string Books
        {
            get { return "books"; }
        }

        /// <summary>
        /// Gets the magazines Uri.
        /// </summary>
        [SupportedProperty(Api.magazines, Range = global::Hydra.Hydra.Collection)]
        [AllowGet(Range = global::Hydra.Hydra.Collection)]
        public string Magazines
        {
            get { return "magazines"; }
        }

        [UsedImplicitly, JsonProperty]
        private JObject Context
        {
            get
            {
                return new JObject(
                    Base.Is(Id),
                    Prefix.Of(typeof(Wbo)),
                    Prefix.Of(typeof(Api)),
                    "magazines".IsProperty(Api.magazines).Type().Id(),
                    "brochures".IsProperty(Api.brochures).Type().Id(),
                    "books".IsProperty(Api.books).Type().Id());
            }
        }

        [UsedImplicitly, JsonProperty]
        private string Type
        {
            get { return Api.EntryPoint; }
        }
    }
}
