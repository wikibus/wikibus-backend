using System.Configuration;
using Hydra.Annotations;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
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
                    new JProperty("@base", ConfigurationManager.AppSettings["baseUrl"]),
                    new JProperty(Wbo.Prefix, Wbo.BaseUri),
                    new JProperty(Api.Prefix, Api.BaseUri),
                    new JObject(new JProperty("magazines", Api.magazines), new JProperty("@type", "@id")),
                    new JObject(new JProperty("brochures", Api.brochures), new JProperty("@type", "@id")),
                    new JObject(new JProperty("books", Api.books), new JProperty("@type", "@id")));
            }
        }

        [UsedImplicitly]
        private string Type
        {
            get { return Api.EntryPoint; }
        }
    }
}
