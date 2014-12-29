using Nancy;
using wikibus.common;

namespace wikibus.nancy
{
    /// <summary>
    /// Serves the <see cref="EntryPoint"/>
    /// </summary>
    public class EntrypointModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntrypointModule"/> class.
        /// </summary>
        public EntrypointModule(IWikibusConfiguration config)
        {
            Get["/"] = route => new EntryPoint(config.BaseApiNamespace);
        }
    }
}
