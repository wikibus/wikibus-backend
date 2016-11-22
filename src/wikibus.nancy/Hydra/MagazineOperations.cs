using Hydra.Discovery.SupportedOperations;
using Wikibus.Sources;

namespace Wikibus.Nancy.Hydra
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
            this.Class.SupportsGet();
            this.Property(e => e.Issues).SupportsGet();
        }
    }
}