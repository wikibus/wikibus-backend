using JsonLD.Entities;
using Newtonsoft.Json.Linq;

namespace wikibus.nancy
{
    /// <summary>
    /// Extension to set up @context for <see cref="EntryPoint"/> model
    /// </summary>
    public static class ContextProviderExtensions
    {
        private const string EntrypointContext = @"{
    '@base': 'http://wikibus.org/',
    'wb': 'http://wikibus.org/ontology#',
    'magazines': { '@id': 'wb:magazines', '@type': '@id' },
    'books': { '@id': 'wb:books', '@type': '@id' },
    'brochures': { '@id': 'wb:brochures', '@type': '@id' }
}";

        /// <summary>
        /// Setups the <see cref="EntryPoint"/> context.
        /// </summary>
        public static void SetupEntrypointContext(this StaticContextProvider contextProvider)
        {
            contextProvider.SetContext(typeof(EntryPoint), JObject.Parse(EntrypointContext));
        }
    }
}
