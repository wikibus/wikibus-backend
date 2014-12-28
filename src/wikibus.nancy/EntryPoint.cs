using System;
using JsonLD.Entities;
using wikibus.nancy.Hydra;

namespace wikibus.nancy
{
    /// <summary>
    /// The API entry point
    /// </summary>
    [Class("http://data.wikibus.org/EntryPoint")]
    public sealed class EntryPoint
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Uri Id
        {
            get
            {
                return new Uri("http://data.wikibus.org/");
            }
        }

        /// <summary>
        /// Gets the brochures Uri.
        /// </summary>
        [SupportedProperty("wb:brochures", Range = "hydra:PagedCollection")]
        [AllowGet]
        public string Brochures
        {
            get { return "brochures"; }
        }

        /// <summary>
        /// Gets the books Uri.
        /// </summary>
        [SupportedProperty("wb:books", Range = "hydra:PagedCollection")]
        [AllowGet]
        public string Books
        {
            get { return "books"; }
        }

        /// <summary>
        /// Gets the magazines Uri.
        /// </summary>
        [SupportedProperty("wb:magazines", Range = "hydra:PagedCollection")]
        [AllowGet]
        public string Magazines
        {
            get { return "magazines"; }
        }
    }
}
