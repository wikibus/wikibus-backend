using System;

namespace Wikibus.Sources.DotNetRDF
{
    /// <summary>
    /// Configuration of wikibus data sources for RDF
    /// </summary>
    public interface ISourcesDatabaseSettings : Sources.ISourcesDatabaseSettings
    {
        /// <summary>
        /// Gets the SPARQL endpoint with Sources RDF data.
        /// </summary>
        Uri SourcesSparqlEndpoint { get; }
    }
}