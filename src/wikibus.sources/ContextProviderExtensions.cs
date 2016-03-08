using Hydra.Resources;
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
        private static readonly JObject CollectionFrame = JObject.Parse(Resource.AsString("Frames.Collection.jsonld"));

        /// <summary>
        /// Setups the sources frames.
        /// </summary>
        public static void SetupSourcesFrames(this StaticFrameProvider frameProvider)
        {
            frameProvider.SetFrame(typeof(Book), JObject.Parse("{ '@type': 'wbo:Book' }"));
            frameProvider.SetFrame(typeof(Brochure), JObject.Parse("{ '@type': 'wbo:Brochure' }"));
            frameProvider.SetFrame(typeof(Issue), JObject.Parse("{ '@type': 'schema:PublicationIssue' }"));
            frameProvider.SetFrame(typeof(Collection<Book>), CollectionFrame);
            frameProvider.SetFrame(typeof(Collection<Brochure>), CollectionFrame);
            frameProvider.SetFrame(typeof(Collection<Magazine>), CollectionFrame);
            frameProvider.SetFrame(typeof(Collection<Issue>), CollectionFrame);
        }
    }
}
