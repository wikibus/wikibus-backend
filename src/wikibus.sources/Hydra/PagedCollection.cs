using System;
using Newtonsoft.Json;

namespace wikibus.sources.Hydra
{
    public class PagedCollection<T>
    {
        public Uri Id { get; set; }

        public long TotalItems { get; set; }

        [JsonProperty("member")]
        public T[] Members { get; set; }
    }
}