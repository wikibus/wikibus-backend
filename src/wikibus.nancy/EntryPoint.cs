using System.ComponentModel;
using Argolis.Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wikibus.Common;
using Wikibus.Common.JsonLd;
using Vocab = JsonLD.Entities.Context.Vocab;

namespace Wikibus.Nancy
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
            this.Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the brochures Uri.
        /// </summary>
        [ReadOnly(true)]
        [Range(global::Vocab.Hydra.Collection)]
        public IriRef Brochures
        {
            get { return (IriRef)"brochures"; }
        }

        /// <summary>
        /// Gets the books Uri.
        /// </summary>
        [ReadOnly(true)]
        [Range(global::Vocab.Hydra.Collection)]
        public IriRef Books
        {
            get { return (IriRef)"books"; }
        }

        /// <summary>
        /// Gets the magazines Uri.
        /// </summary>
        [ReadOnly(true)]
        [Range(global::Vocab.Hydra.Collection)]
        public IriRef Magazines
        {
            get { return (IriRef)"magazines"; }
        }

        [UsedImplicitly]
        private static JObject Context
        {
            get
            {
                return new JObject(
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

        [UsedImplicitly]
        private static JObject GetContext(EntryPoint p)
        {
            var context = Context;
            context.AddFirst(Base.Is(p.Id));
            return context;
        }
    }
}
