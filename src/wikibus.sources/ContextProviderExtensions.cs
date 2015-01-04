using System;
using Hydra;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using Resourcer;

namespace wikibus.sources
{
    /// <summary>
    /// Extension to set up @context for source models
    /// </summary>
    public static class ContextProviderExtensions
    {
        private static readonly JObject PagedCollectionFrame = JObject.Parse(Resource.AsString("Frames.PagedCollection.json"));

        /// <summary>
        /// Setups the sources frames.
        /// </summary>
        public static void SetupSourcesFrames(this StaticFrameProvider frameProvider)
        {
            frameProvider.SetFrame(typeof(Book), JObject.Parse("{ '@type': 'wbo:Book' }"));
            frameProvider.SetFrame(typeof(Brochure), JObject.Parse("{ '@type': 'wbo:Brochure' }"));
            frameProvider.SetFrame(typeof(Issue), JObject.Parse("{ '@type': 'sch:PublicationIssue' }"));
            frameProvider.SetFrame(typeof(PagedCollection<Book>), PagedCollectionFrame);
            frameProvider.SetFrame(typeof(PagedCollection<Brochure>), PagedCollectionFrame);
            frameProvider.SetFrame(typeof(PagedCollection<Magazine>), PagedCollectionFrame);
        }
    }
}
