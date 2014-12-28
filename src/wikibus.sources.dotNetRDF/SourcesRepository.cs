using System;
using Hydra;
using JsonLD.Entities;
using NullGuard;
using Resourcer;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// dotNetRDF SPARQL repository of sources
    /// </summary>
    [NullGuard(ValidationFlags.ReturnValues)]
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
        public Magazine GetMagazine(Uri identifier)
        {
            return Get<Magazine>(identifier);
        }

        /// <inheritdoc />
        public Brochure GetBrochure(Uri identifier)
        {
            return Get<Brochure>(identifier);
        }

        /// <inheritdoc />
        public Book GetBook(Uri identifier)
        {
            return Get<Book>(identifier);
        }

        /// <inheritdoc />
        public PagedCollection<Book> GetBooks(int page)
        {
            return GetAll<Book>(page);
        }

        /// <inheritdoc />
        public PagedCollection<Brochure> GetBrochures(int page)
        {
            return GetAll<Brochure>(page);
        }

        /// <inheritdoc />
        public PagedCollection<Magazine> GetMagazines(int page)
        {
            return GetAll<Magazine>(page);
        }

        private static Uri GetCollectionUri(Type type)
        {
            string collectionName;
            switch (type.Name)
            {
                case "Book":
                    collectionName = "books";
                    break;
                case "Brochure":
                    collectionName = "brochures";
                    break;
                case "Magazine":
                    collectionName = "magazines";
                    break;
                default:
                    throw new ArgumentException(string.Format("Unrecognized entity type {0}", type), "type");
            }

            return new Uri(string.Format("http://wikibus.org/{0}", collectionName));
        }

        private static Uri GetTypeUri(Type type)
        {
            return new Uri(string.Format("http://wikibus.org/ontology#{0}", type.Name));
        }

        private T Get<T>(Uri uri) where T : class
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

        private PagedCollection<T> GetAll<T>(int page, int pageSize = 10) where T : class
        {
            var query = new SparqlParameterizedString(Resource.AsString("SparqlQueries.GetSourcesPage.rq"));
            query.SetUri("type", GetTypeUri(typeof(T)));
            query.SetUri("container", GetCollectionUri(typeof(T)));
            query.SetLiteral("limit", pageSize);
            query.SetLiteral("offset", (page - 1) * pageSize);
            var graph = (IGraph)_queryProcessor.ProcessQuery(_parser.ParseFromString(query.ToString()));

            if (graph.Triples.Count > 0)
            {
                var dataset = StringWriter.Write(graph, new NTriplesWriter(NTriplesSyntax.Rdf11));

                var collection = _serializer.Deserialize<PagedCollection<T>>(dataset);
                collection.ItemsPerPage = pageSize;
                collection.CurrentPage = page;

                return collection;
            }

            return new PagedCollection<T>();
        }
    }
}
