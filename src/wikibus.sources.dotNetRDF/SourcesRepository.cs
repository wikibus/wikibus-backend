using System;
using JsonLD.Entities;
using NullGuard;
using Resourcer;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using wikibus.sources.Hydra;

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
        [return: AllowNull]
        public T Get<T>(Uri uri) where T : Source
        {
            var construct = "CONSTRUCT { @source ?p ?o. ?o ?p1 ?o1 } WHERE { @source ?p ?o . OPTIONAL { ?o ?p1 ?o1 } . @source a @type . }";
            var query = new SparqlParameterizedString(construct);
            query.SetUri("source", uri);
            query.SetUri("type", GetTypeUri(typeof(T)));
            var graph = (IGraph)_queryProcessor.ProcessQuery(_parser.ParseFromString(query.ToString()));

            if (graph.Triples.Count > 0)
            {
                var dataset = StringWriter.Write(graph, new NTriplesWriter(NTriplesSyntax.Rdf11));

                return _serializer.Deserialize<T>(dataset);
            }

            return null;
        }

        /// <inheritdoc />
        public PagedCollection<T> GetAll<T>(int page, int pageSize = 10) where T : Source
        {
            var query = new SparqlParameterizedString(Resource.AsString("SparqlQueries.GetSource.rq"));
            query.SetUri("type", GetTypeUri(typeof(T)));
            query.SetUri("container", GetCollectionUri(typeof(T)));
            query.SetLiteral("limit", pageSize);
            query.SetLiteral("offset", (page - 1) * pageSize);
            var graph = (IGraph)_queryProcessor.ProcessQuery(_parser.ParseFromString(query.ToString()));

            if (graph.Triples.Count > 0)
            {
                var dataset = StringWriter.Write(graph, new NTriplesWriter(NTriplesSyntax.Rdf11));

                return _serializer.Deserialize<PagedCollection<T>>(dataset);
            }

            return new PagedCollection<T>();
        }

        private Uri GetCollectionUri(Type type)
        {
            string collectionName = "books";
            return new Uri(string.Format("http://wikibus.org/{0}", collectionName));
        }

        private Uri GetTypeUri(Type type)
        {
            return new Uri(string.Format("http://wikibus.org/ontology#{0}", type.Name));
        }
    }
}
