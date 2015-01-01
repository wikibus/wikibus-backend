using System.Configuration;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using Resourcer;

namespace wikibus.nancy
{
    /// <summary>
    /// Extension to set up @context for <see cref="EntryPoint"/> model
    /// </summary>
    public static class ContextProviderExtensions
    {
        private const string EntrypointContext = @"{
    'wb': 'http://wikibus.org/ontology#',
    'api': 'http://data.wikibus.org/',
    'magazines': { '@id': 'wb:magazines', '@type': '@id' },
    'books': { '@id': 'wb:books', '@type': '@id' },
    'brochures': { '@id': 'wb:brochures', '@type': '@id' }
}";

        /// <summary>
        /// Setups the <see cref="EntryPoint"/> context.
        /// </summary>
        public static void SetupEntrypointContext(this StaticContextProvider contextProvider)
        {
            JObject jObject = JObject.Parse(EntrypointContext);
            jObject["@base"] = ConfigurationManager.AppSettings["baseUrl"];
            contextProvider.SetContext(typeof(EntryPoint), jObject);
        }

        /// <summary>
        /// Setups the <see cref="EntryPoint"/> context.
        /// </summary>
        public static void SetupEntrypointContext(this StaticFrameProvider frameProvider)
        {
            frameProvider.SetFrame(typeof(WikibusApi), JObject.Parse("{ '@type': 'ApiDocumentation' }"));
        }
    }
}
