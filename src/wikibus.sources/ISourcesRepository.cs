using System;

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
        T Get<T>(Uri uri) where T : Source;
    }
}
