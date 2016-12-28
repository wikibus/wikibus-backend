using Argolis.Hydra.Discovery.SupportedOperations;
using Wikibus.Sources;

namespace Wikibus.Nancy.Hydra
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
            this.Class.SupportsGet();
            this.Property(e => e.Magazine).SupportsGet();
        }
    }
}