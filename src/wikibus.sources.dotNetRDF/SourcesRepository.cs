using System;
using Hydra;
using JsonLD.Entities;
using NullGuard;
using Resourcer;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using wikibus.common.Vocabularies;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// dotNetRDF SPARQL repository of sources
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.ReturnValues)]
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
        public PagedCollection<Book> GetBooks(Uri identifier, int page)
        {
            return GetAll<Book>(identifier, page);
        }

        /// <inheritdoc />
        public PagedCollection<Brochure> GetBrochures(Uri identifier, int page)
        {
            return GetAll<Brochure>(identifier, page);
        }

        /// <inheritdoc />
        public PagedCollection<Magazine> GetMagazines(Uri identifier, int page)
        {
            return GetAll<Magazine>(identifier, page);
        }

        /// <inheritdoc />
        public Collection<Issue> GetMagazineIssues(Uri identifier)
        {
            return Get<Collection<Issue>>(identifier);
        }

        /// <inheritdoc />
        public Issue GetIssue(Uri identifier)
        {
            return Get<Issue>(identifier);
        }

        private static Uri GetTypeUri(Type type)
        {
            if (type == typeof(Issue))
            {
                return new Uri(Schema.PublicationIssue);
            }

            return new Uri(string.Format("http://wikibus.org/ontology#{0}", type.Name));
        }

        private T Get<T>(Uri uri) where T : class
        {
            var query = new SparqlParameterizedString(Resource.AsString("SparqlQueries.GetSingle.rq"));
            query.SetUri("source", uri);
            var graph = (IGraph)_queryProcessor.ProcessQuery(_parser.ParseFromString(query.ToString()));

            if (graph.Triples.Count > 0)
            {
                var dataset = StringWriter.Write(graph, new NTriplesWriter(NTriplesSyntax.Rdf11));

                return _serializer.Deserialize<T>(dataset);
            }

            return null;
        }

        private PagedCollection<T> GetAll<T>(Uri collectionUri, int page, int pageSize = 10) where T : class
        {
            var query = new SparqlParameterizedString(Resource.AsString("SparqlQueries.GetSourcesPage.rq"));
            query.SetUri("type", GetTypeUri(typeof(T)));
            query.SetUri("collection", collectionUri);
            query.SetLiteral("page", page);
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
