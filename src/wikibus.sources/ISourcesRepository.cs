using System;
using System.Threading.Tasks;
using Hydra.Resources;
using Wikibus.Sources.Filters;

namespace Wikibus.Sources
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
        Task<Magazine> GetMagazine(Uri identifier);

        /// <summary>
        /// Gets the brochure.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        Task<Brochure> GetBrochure(Uri identifier);

        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        Task<Book> GetBook(Uri identifier);

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="filters">Filtering of the collection</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Number of books to fetch</param>
        Task<Collection<Book>> GetBooks(Uri identifier, BookFilters filters, int page, int pageSize = 10);

        /// <summary>
        /// Gets the brochures.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="filters">Filtering of the collection</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Number of brochures to fetch</param>
        Task<Collection<Brochure>> GetBrochures(Uri identifier, BrochureFilters filters, int page, int pageSize = 10);

        /// <summary>
        /// Gets the magazines.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="filters">Filtering of the collection</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Number of magazines to fetch</param>
        Task<Collection<Magazine>> GetMagazines(Uri identifier, MagazineFilters filters, int page, int pageSize = 10);

        /// <summary>
        /// Gets the magazine issues.
        /// </summary>
        Task<Collection<Issue>> GetMagazineIssues(Uri uri);

        /// <summary>
        /// Gets the issue.
        /// </summary>
        Task<Issue> GetIssue(Uri identifier);
    }
}
