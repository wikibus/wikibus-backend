using Argolis.Hydra.Discovery.SupportedOperations;
using Wikibus.Sources;

namespace Wikibus.Nancy.Hydra
{
    /// <summary>
    /// Sets up operations supported by <see cref="Book"/> class
    /// </summary>
    public class BookOperations : SupportedOperations<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookOperations"/> class.
        /// </summary>
        public BookOperations()
        {
            this.Class.SupportsGet("Gets the book");
        }
    }
}