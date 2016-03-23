using Hydra.Discovery.SupportedOperations;
using wikibus.sources;

namespace wikibus.nancy.Hydra
{
    /// <summary>
    /// Sets up operations supported by <see cref="Issue"/> class
    /// </summary>
    public class IssueOperations : SupportedOperations<Issue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueOperations"/> class.
        /// </summary>
        public IssueOperations()
        {
            SupportsGet();
            Property(e => e.Magazine).SupportsGet();
        }
    }
}