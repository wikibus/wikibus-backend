using JsonLD.Entities;
using Newtonsoft.Json.Linq;

namespace wikibus.nancy
{
    /// <summary>
    /// Extension to set up @context for <see cref="EntryPoint"/> model
    /// </summary>
    public static class ContextProviderExtensions
    {
        /// <summary>
        /// Setups the <see cref="EntryPoint"/> context.
        /// </summary>
        public static void SetupEntrypointContext(this StaticFrameProvider frameProvider)
        {
            frameProvider.SetFrame(typeof(WikibusApi), JObject.Parse("{ '@type': 'ApiDocumentation' }"));
        }
    }
}
