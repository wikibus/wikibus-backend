using System;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using Resourcer;
using wikibus.sources.Hydra;

namespace wikibus.sources
{
    /// <summary>
    /// Extension to set up @context for source models
    /// </summary>
    public static class ContextProviderExtensions
    {
        private static readonly JObject BrochureContext = JObject.Parse(Resource.AsString("Contexts.Brochure.json"));
        private static readonly JObject MagazineContext = JObject.Parse(Resource.AsString("Contexts.Magazine.json"));
        private static readonly JObject PagedCollectionContext = JObject.Parse(Resource.AsString("Contexts.PagedCollection.json"));
        private static readonly JObject PagedCollectionFrame = JObject.Parse(Resource.AsString("Frames.PagedCollection.json"));

        /// <summary>
        /// Setups the sources contexts.
        /// </summary>
        public static void SetupSourcesContexts(this StaticContextProvider contextProvider)
        {
            contextProvider.SetContext(typeof(Brochure), BrochureContext);
            contextProvider.SetContext(typeof(Book), BrochureContext);
            contextProvider.SetContext(typeof(Issue), BrochureContext);
            contextProvider.SetContext(typeof(Magazine), MagazineContext);
            contextProvider.SetContext(typeof(PagedCollection<Book>), contextProvider.CreateCollectionContext(typeof(Book)));
            contextProvider.SetContext(typeof(PagedCollection<Brochure>), contextProvider.CreateCollectionContext(typeof(Brochure)));
            contextProvider.SetContext(typeof(PagedCollection<Magazine>), contextProvider.CreateCollectionContext(typeof(Magazine)));
        }

        /// <summary>
        /// Setups the sources frames.
        /// </summary>
        public static void SetupSourcesFrames(this StaticFrameProvider frameProvider)
        {
            frameProvider.SetFrame(typeof(Book), JObject.Parse("{ '@type': 'http://wikibus.org/ontology#Book' }"));
            frameProvider.SetFrame(typeof(Brochure), JObject.Parse("{ '@type': 'http://wikibus.org/ontology#Brochure' }"));
            frameProvider.SetFrame(typeof(PagedCollection<Book>), PagedCollectionFrame);
            frameProvider.SetFrame(typeof(PagedCollection<Brochure>), PagedCollectionFrame);
            frameProvider.SetFrame(typeof(PagedCollection<Magazine>), PagedCollectionFrame);
        }

        private static JArray CreateCollectionContext(this IContextProvider contextProvider, Type modelType)
        {
            return new JArray("http://www.w3.org/ns/hydra/context.jsonld", contextProvider.GetContext(modelType), PagedCollectionContext);
        }
    }
}
