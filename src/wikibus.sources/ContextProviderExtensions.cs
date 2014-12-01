using JsonLD.Entities;
using Newtonsoft.Json.Linq;

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
            contextProvider.SetContext(typeof(Brochure), JObject.Parse("{ 'title': 'http://purl.org/dc/terms/title' }"));
        }
    }
}
