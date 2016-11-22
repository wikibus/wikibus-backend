using Hydra.Discovery.SupportedOperations;
using Wikibus.Sources;

namespace Wikibus.Nancy.Hydra
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
            this.Class.SupportsGet();
        }
    }
}