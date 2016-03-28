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
            Class.SupportsGet("Gets the API entrypoint", "The entrypoint is the the API starts");

            Property(e => e.Books).SupportsGet("Gets the collection of books (paged)");
            Property(e => e.Brochures).SupportsGet("Gets the collection of brochures (paged)");
            Property(e => e.Magazines).SupportsGet("Gets the collection of magazines");
        }
    }
}
