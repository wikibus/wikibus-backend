using Hydra.Discovery.SupportedOperations;
using wikibus.sources;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Sets up operations supported by <see cref="Brochure"/> class
    /// </summary>
    public class BrochureOperations : SupportedOperations<Brochure>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrochureOperations"/> class.
        /// </summary>
        public BrochureOperations()
        {
            Class.SupportsGet();
        }
    }
}