using System;
using JsonLD.Core;
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
        private readonly SparqlQueryParser _parser = new SparqlQueryParser();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesRepository"/> class.
        /// </summary>
        /// <param name="queryProcessor">The query processor.</param>
        public SourcesRepository(ISparqlQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        /// <inheritdoc />
        public T Get<T>(Uri uri) where T : Source
        {
            var construct = "CONSTRUCT { @s ?p ?o } WHERE { @s ?p ?o . @s a <http://wikibus.org/ontology#folder> . }";
            var query = new SparqlParameterizedString(construct);
            query.SetUri("s", uri);
            var triples = (IGraph)_queryProcessor.ProcessQuery(_parser.ParseFromString(query.ToString()));

            var dataset = StringWriter.Write(triples, new NTriplesWriter(NTriplesSyntax.Rdf11));
            var result = JsonLdProcessor.FromRDF(dataset);
            var context = JObject.Parse("{ 'title': 'http://purl.org/dc/terms/title' }");
            result = JsonLdProcessor.Compact(result, context, new JsonLdOptions("http://wikibus.org/"));

            return result.ToObject<T>();
        }
    }
}
