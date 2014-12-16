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
        /// <summary>
        /// Setups the sources contexts.
        /// </summary>
        public static void SetupSourcesContexts(this StaticContextProvider contextProvider)
        {
            contextProvider.SetContext(typeof(Brochure), JObject.Parse(Resource.AsString("Contexts.Brochure.json")));
            contextProvider.SetContext(typeof(Book), JObject.Parse(Resource.AsString("Contexts.Brochure.json")));
            contextProvider.SetContext(
                typeof(PagedCollection<Book>),
                new JArray(
                    "http://www.w3.org/ns/hydra/context.jsonld",
                    contextProvider.GetContext(typeof(Book)),
                    JObject.Parse(Resource.AsString("Contexts.PagedCollection.json"))));
        }

        /// <summary>
        /// Setups the sources frames.
        /// </summary>
        public static void SetupSourcesFrames(this StaticFrameProvider frameProvider)
        {
            frameProvider.SetFrame(typeof(Book), JObject.Parse("{ '@type': 'http://wikibus.org/ontology#Book' }"));
            frameProvider.SetFrame(
                typeof(PagedCollection<Book>),
                JObject.Parse("{ '@type': 'http://www.w3.org/ns/hydra/core#PagedCollection' }"));
        }
    }
}
