using Hydra.Discovery.SupportedOperations;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Sets up operations supported by <see cref="EntryPoint"/> class
    /// </summary>
    public class EntrypointOperations : SupportedOperations<EntryPoint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntrypointOperations"/> class.
        /// </summary>
        public EntrypointOperations()
        {
            SupportsGet();

            Property(e => e.Books).SupportsGet();
            Property(e => e.Brochures).SupportsGet();
            Property(e => e.Magazines).SupportsGet();
        }
    }
}
