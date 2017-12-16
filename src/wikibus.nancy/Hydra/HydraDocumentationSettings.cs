using Argolis.Hydra;
using JsonLD.Entities;
using Wikibus.Common;

namespace Wikibus.Nancy.Hydra
{
    /// <summary>
    /// Hydra API documentation settings
    /// </summary>
    /// <seealso cref="IHydraDocumentationSettings" />
    public class HydraDocumentationSettings : IHydraDocumentationSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraDocumentationSettings"/> class.
        /// </summary>
        public HydraDocumentationSettings(IWikibusConfiguration configuration)
        {
            this.EntryPoint = (IriRef)configuration.BaseResourceNamespace;
        }

        /// <summary>
        /// Gets the documentation path.
        /// </summary>
        public string DocumentationPath
        {
            get { return "doc"; }
        }

        /// <inheritdoc/>
        public IriRef EntryPoint { get; }
    }
}