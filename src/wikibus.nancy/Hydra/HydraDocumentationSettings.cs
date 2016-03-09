using Hydra.Nancy;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Hydra API documentation settings
    /// </summary>
    /// <seealso cref="IHydraDocumentationSettings" />
    public class HydraDocumentationSettings : IHydraDocumentationSettings
    {
        /// <summary>
        /// Gets the documentation path.
        /// </summary>
        public string DocumentationPath
        {
            get { return "doc"; }
        }
    }
}