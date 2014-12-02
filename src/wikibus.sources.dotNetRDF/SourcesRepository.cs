using System;
using JsonLD.Core;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// dotNetRDF SPARQL repository of sources
    /// </summary>
    public class SourcesRepository : ISourcesRepository
    {
        private readonly ISparqlQueryProcessor _queryProcessor;
        private readonly IEntitySerializer _serializer;
        private readonly SparqlQueryParser _parser = new SparqlQueryParser();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesRepository"/> class.
        /// </summary>
        /// <param name="queryProcessor">The query processor.</param>
        /// <param name="serializer">JSON-LD serializer</param>
        public SourcesRepository(ISparqlQueryProcessor queryProcessor, IEntitySerializer serializer)
        {
            _queryProcessor = queryProcessor;
            _serializer = serializer;
        }

        /// <inheritdoc />
        public T Get<T>(Uri uri) where T : Source
        {
            var construct = "CONSTRUCT { @s ?p ?o } WHERE { @s ?p ?o . @s a <http://wikibus.org/ontology#Brochure> . }";
            var query = new SparqlParameterizedString(construct);
            query.SetUri("s", uri);
            var graph = (IGraph)_queryProcessor.ProcessQuery(_parser.ParseFromString(query.ToString()));

            if (graph.Triples.Count > 0)
            {
                var dataset = StringWriter.Write(graph, new NTriplesWriter(NTriplesSyntax.Rdf11));

                return _serializer.Deserialize<T>(dataset);
            }

            return null;
        }
    }
}
