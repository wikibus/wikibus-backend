using Argolis.Hydra.Discovery.SupportedOperations;

namespace Wikibus.Nancy.Hydra
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
            this.Class.SupportsGet("Gets the API entrypoint", "The entrypoint is the the API starts");

            this.Property(e => e.Books).SupportsGet("Gets the collection of books (paged)");
            this.Property(e => e.Brochures).SupportsGet("Gets the collection of brochures (paged)");
            this.Property(e => e.Magazines).SupportsGet("Gets the collection of magazines");
        }
    }
}
