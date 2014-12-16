using System;
using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;

namespace wikibus.sources.Hydra
{
    /// <summary>
    /// Hydra paged collection
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    [Class("http://www.w3.org/ns/hydra/core#PagedCollection")]
    [NullGuard(ValidationFlags.ReturnValues)]
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
