using System;
using Hydra;
using wikibus.sources.Hydra;

namespace wikibus.sources
{
    /// <summary>
    /// Facade for accessing sources
    /// </summary>
    public interface ISourcesRepository
    {
        /// <summary>
        /// Gets the magazine.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        Magazine GetMagazine(Uri identifier);

        /// <summary>
        /// Gets the brochure.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        Brochure GetBrochure(Uri identifier);

        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        Book GetBook(Uri identifier);

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <param name="page">The page.</param>
        PagedCollection<Book> GetBooks(int page);

        /// <summary>
        /// Gets the brochures.
        /// </summary>
        /// <param name="page">The page.</param>
        PagedCollection<Brochure> GetBrochures(int page);

        /// <summary>
        /// Gets the magazines.
        /// </summary>
        /// <param name="page">The page.</param>
        PagedCollection<Magazine> GetMagazines(int page);
    }
}
