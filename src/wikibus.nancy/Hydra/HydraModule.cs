using Nancy;
using Nancy.Responses;
using Resourcer;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Server Hydra API documentation
    /// </summary>
    public class HydraModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraModule"/> class.
        /// </summary>
         public HydraModule() : base("doc")
         {
             Get["/"] = route => new StreamResponse(() => Resource.AsStream("ApiDocumentation.json"), "application/ld+json");
         }
    }
}
