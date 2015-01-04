using System.Configuration;
using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;
using wikibus.common.JsonLd;
using wikibus.common.Vocabularies;

namespace wikibus.nancy
{
    /// <summary>
    /// The API entry point
    /// </summary>
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
        [SupportedProperty(Api.brochures)]
        [AllowGet(Range = Hydra.Hydra.PagedCollection)]
        public string Brochures
        {
            get { return "brochures"; }
        }

        /// <summary>
        /// Gets the books Uri.
        /// </summary>
        [SupportedProperty(Api.books, Range = Hydra.Hydra.PagedCollection)]
        [AllowGet]
        public string Books
        {
            get { return "books"; }
        }

        /// <summary>
        /// Gets the magazines Uri.
        /// </summary>
        [SupportedProperty(Api.magazines, Range = Hydra.Hydra.PagedCollection)]
        [AllowGet]
        public string Magazines
        {
            get { return "magazines"; }
        }

        [UsedImplicitly]
        private static JObject Context
        {
            get
            {
                return new JObject(
                    Base.Is(ConfigurationManager.AppSettings["baseUrl"]),
                    Prefix.Of(typeof(Wbo)),
                    Prefix.Of(typeof(Api)),
                    "magazines".IsProperty(Api.magazines).Type().Id(),
                    "brochures".IsProperty(Api.brochures).Type().Id(),
                    "books".IsProperty(Api.books).Type().Id());
            }
        }

        [UsedImplicitly]
        private string Type
        {
            get { return Api.EntryPoint; }
        }
    }
}
