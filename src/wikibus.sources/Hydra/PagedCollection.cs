using System;
using Newtonsoft.Json;

namespace wikibus.sources.Hydra
{
    /// <summary>
    /// Hydra paged collection
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    public class PagedCollection<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        [JsonProperty("member")]
        public T[] Members { get; set; }
    }
}
