using Hydra.Discovery.SupportedOperations;
using wikibus.sources;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Sets up operations supported by <see cref="Magazine"/> class
    /// </summary>
    public class MagazineOperations : SupportedOperations<Magazine>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagazineOperations"/> class.
        /// </summary>
        public MagazineOperations()
        {
            Class.SupportsGet();
            Property(e => e.Issues).SupportsGet();
        }
    }
}