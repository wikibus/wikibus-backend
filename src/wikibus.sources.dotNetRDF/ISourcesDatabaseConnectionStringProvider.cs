namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// Configuration of wikibus data sources
    /// </summary>
    public interface ISourcesDatabaseConnectionStringProvider
    {
        /// <summary>
        /// Gets the wikibus database connection string.
        /// </summary>
        string ConnectionString { get; }
    }
}