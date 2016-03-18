using System.Collections.Generic;
using Hydra;
using Hydra.DocumentationDiscovery;
using JsonLD.Entities;
using wikibus.common;
using wikibus.sources;

namespace wikibus.nancy.Hydra
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
            EntryPoint = (IriRef)configuration.BaseResourceNamespace;
        }

        /// <summary>
        /// Gets the documentation path.
        /// </summary>
        public string DocumentationPath
        {
            get { return "doc"; }
        }
        
        /// <inheritdoc/>
        public IEnumerable<IDocumentedTypeSelector> Sources
        {
            get
            {
                yield return new AssemblyAnnotatedTypeSelector(typeof(Source).Assembly);
            }
        }

        /// <inheritdoc/>
        public IriRef EntryPoint { get; }
    }
}