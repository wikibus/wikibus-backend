using Hydra.Discovery.SupportedOperations;
using wikibus.sources;

namespace wikibus.nancy.Hydra
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
            Class.SupportsGet("Gets the book");
        }
    }
}