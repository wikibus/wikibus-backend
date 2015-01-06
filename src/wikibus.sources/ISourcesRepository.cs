using System;
using Hydra;

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
        /// <param name="identifier">The identifier.</param>
        /// <param name="page">The page.</param>
        PagedCollection<Book> GetBooks(Uri identifier, int page);

        /// <summary>
        /// Gets the brochures.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="page">The page.</param>
        PagedCollection<Brochure> GetBrochures(Uri identifier, int page);

        /// <summary>
        /// Gets the magazines.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="page">The page.</param>
        PagedCollection<Magazine> GetMagazines(Uri identifier, int page);

        /// <summary>
        /// Gets the magazine issues.
        /// </summary>
        Collection<Issue> GetMagazineIssues(Uri uri);

        /// <summary>
        /// Gets the issue.
        /// </summary>
        Issue GetIssue(Uri identifier);
    }
}
