using System;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// Configuration of wikibus data sources
    /// </summary>
    public interface ISourcesDatabaseSettings
    {
        /// <summary>
        /// Gets the wikibus database connection string.
        /// </summary>
        string ConnectionString { get; }
        
        /// <summary>
        /// Gets the SPARQL endpoint with Sources RDF data.
        /// </summary>
        Uri SourcesSparqlEndpoint { get; }
    }
}