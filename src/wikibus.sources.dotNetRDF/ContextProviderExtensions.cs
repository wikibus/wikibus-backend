using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using Resourcer;

namespace wikibus.sources.dotNetRDF
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
        }
    }
}
