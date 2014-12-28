using System;
using JsonLD.Entities;

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
        public string Brochures
        {
            get { return "brochures"; }
        }

        /// <summary>
        /// Gets the books Uri.
        /// </summary>
        public string Books
        {
            get { return "books"; }
        }

        /// <summary>
        /// Gets the magazines Uri.
        /// </summary>
        public string Magazines
        {
            get { return "magazines"; }
        }
    }
}
