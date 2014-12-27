using System;
using wikibus.sources.Hydra;

namespace wikibus.sources
{
    /// <summary>
    /// Facade for accessing sources
    /// </summary>
    public interface ISourcesRepository
    {
        /// <summary>
        /// Gets the specified resource.
        /// </summary>
        /// <param name="uri">The source identifier.</param>
        /// <typeparam name="T">type of source</typeparam>
        T Get<T>(Uri uri) where T : class;

        /// <summary>
        /// Gets all resources of type <typeparamref name="T" />
        /// </summary>
        /// <typeparam name="T">Source type</typeparam>
        PagedCollection<T> GetAll<T>(int page, int pageSize = 10) where T : class;
    }
}
