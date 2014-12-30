using Hydra.Annotations;
using JsonLD.Entities;

namespace wikibus.nancy
{
    /// <summary>
    /// The API entry point
    /// </summary>
    [Class("api:EntryPoint")]
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
        [SupportedProperty("wb:brochures")]
        [AllowGet(Range = Hydra.Hydra.PagedCollection)]
        public string Brochures
        {
            get { return "brochures"; }
        }

        /// <summary>
        /// Gets the books Uri.
        /// </summary>
        [SupportedProperty("wb:books", Range = Hydra.Hydra.PagedCollection)]
        [AllowGet]
        public string Books
        {
            get { return "books"; }
        }

        /// <summary>
        /// Gets the magazines Uri.
        /// </summary>
        [SupportedProperty("wb:magazines", Range = Hydra.Hydra.PagedCollection)]
        [AllowGet]
        public string Magazines
        {
            get { return "magazines"; }
        }
    }
}
