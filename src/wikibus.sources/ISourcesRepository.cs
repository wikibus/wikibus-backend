﻿using System;
using Hydra.Resources;

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
        /// <param name="pageSize">Number of books to fetch</param>
        Collection<Book> GetBooks(Uri identifier, int page, int pageSize = 10);

        /// <summary>
        /// Gets the brochures.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Number of brochures to fetch</param>
        Collection<Brochure> GetBrochures(Uri identifier, int page, int pageSize = 10);

        /// <summary>
        /// Gets the magazines.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Number of magazines to fetch</param>
        Collection<Magazine> GetMagazines(Uri identifier, int page, int pageSize = 10);

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
