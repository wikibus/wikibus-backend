namespace Wikibus.Sources
{
    /// <summary>
    /// Settings of the sources SQL database
    /// </summary>
    public interface ISourcesDatabaseSettings
    {
        /// <summary>
        /// Gets the wikibus database connection string.
        /// </summary>
        string ConnectionString { get; }
    }
}