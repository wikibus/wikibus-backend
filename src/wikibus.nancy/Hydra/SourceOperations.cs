using Hydra.Discovery.SupportedOperations;
using wikibus.sources;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Sets up operations supported by <see cref="Source"/> class
    /// </summary>
    public class SourceOperations : SupportedOperations<Source>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceOperations"/> class.
        /// </summary>
        public SourceOperations()
        {
            Class.SupportsGet("Gets the source");
        }
    }
}