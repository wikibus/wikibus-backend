using Nancy;
using Wikibus.Common;

namespace Wikibus.Nancy
{
    /// <summary>
    /// Serves the <see cref="EntryPoint"/>
    /// </summary>
    public sealed class EntrypointModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntrypointModule"/> class.
        /// </summary>
        public EntrypointModule(IWikibusConfiguration config)
        {
            this.Get("/", route => new EntryPoint(config.BaseResourceNamespace));
        }
    }
}
