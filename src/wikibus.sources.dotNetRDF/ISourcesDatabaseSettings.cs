using System;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// Configuration of wikibus data sources for RDF
    /// </summary>
    public interface ISourcesDatabaseSettings : sources.ISourcesDatabaseSettings
    {
        /// <summary>
        /// Gets the SPARQL endpoint with Sources RDF data.
        /// </summary>
        Uri SourcesSparqlEndpoint { get; }
    }
}