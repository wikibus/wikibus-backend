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
        /// Gets or sets the page size.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets the next page URI.
        /// </summary>
        public Uri NextPage
        {
            get
            {
                var builder = new UriBuilder(Id);
                builder.Query = "page=" + (Page + 1);
                return builder.Uri;
            }
        }

        /// <summary>
        /// Gets the last page URI.
        /// </summary>
        public Uri LastPage
        {
            get
            {
                var builder = new UriBuilder(Id);
                builder.Query = "page=" + Math.Ceiling((decimal)TotalItems / ItemsPerPage);

                return builder.Uri;
            }
        }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        [JsonProperty("member")]
        public T[] Members { get; set; }
    }
}
