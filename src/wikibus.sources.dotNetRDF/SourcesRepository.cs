using System;
using Hydra.Resources;
using JsonLD.Entities;
using NullGuard;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using Vocab;
using Wikibus.Sources.Filters;
using Resource = Resourcer.Resource;

namespace Wikibus.Sources.DotNetRDF
{
    /// <summary>
    /// dotNetRDF SPARQL repository of sources
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.ReturnValues)]
    public class SourcesRepository : ISourcesRepository
    {
        private readonly Lazy<ISparqlQueryProcessor> queryProcessor;
        private readonly IEntitySerializer serializer;
        private readonly SparqlQueryParser parser = new SparqlQueryParser();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesRepository"/> class.
        /// </summary>
        /// <param name="queryProcessor">The query processor.</param>
        /// <param name="serializer">JSON-LD serializer</param>
        public SourcesRepository(Lazy<ISparqlQueryProcessor> queryProcessor, IEntitySerializer serializer)
        {
            this.queryProcessor = queryProcessor;
            this.serializer = serializer;
        }

        /// <inheritdoc />
        public Magazine GetMagazine(Uri identifier)
        {
            return this.Get<Magazine>(identifier);
        }

        /// <inheritdoc />
        public Brochure GetBrochure(Uri identifier)
        {
            return this.Get<Brochure>(identifier);
        }

        /// <inheritdoc />
        public Book GetBook(Uri identifier)
        {
            return this.Get<Book>(identifier);
        }

        /// <inheritdoc />
        public Collection<Book> GetBooks(Uri identifier, BookFilters filters, int page, int pageSize = 10)
        {
            return this.GetAll<Book, Collection<Book>>(identifier, page, pageSize);
        }

        /// <inheritdoc />
        public Collection<Brochure> GetBrochures(Uri identifier, BrochureFilters filters, int page, int pageSize = 10)
        {
            return this.GetAll<Brochure, Collection<Brochure>>(identifier, page, pageSize);
        }

        /// <inheritdoc />
        public Collection<Magazine> GetMagazines(Uri identifier, MagazineFilters filters, int page, int pageSize = 10)
        {
            return this.GetAll<Magazine, Collection<Magazine>>(identifier, page, pageSize);
        }

        /// <inheritdoc />
        public Collection<Issue> GetMagazineIssues(Uri identifier)
        {
            var magazineIssues = this.Get<Collection<Issue>>(identifier);
            if (magazineIssues != null)
            {
                magazineIssues.TotalItems = magazineIssues.Members.Length;
                return magazineIssues;
            }

            return new Collection<Issue>();
        }

        /// <inheritdoc />
        public Issue GetIssue(Uri identifier)
        {
            return this.Get<Issue>(identifier);
        }

        private static Uri GetTypeUri(Type type)
        {
            if (type == typeof(Issue))
            {
                return new Uri(Schema.PublicationIssue);
            }

            return new Uri(string.Format("http://wikibus.org/ontology#{0}", type.Name));
        }

        private T Get<T>(Uri uri)
            where T : class
        {
            var query = new SparqlParameterizedString(Resource.AsString("SparqlQueries.GetSingle.rq"));
            query.SetUri("source", uri);
            var graph = (IGraph)this.queryProcessor.Value.ProcessQuery(this.parser.ParseFromString(query.ToString()));

            if (graph.Triples.Count > 0)
            {
                var dataset = StringWriter.Write(graph, new NTriplesWriter(NTriplesSyntax.Rdf11));

                return this.serializer.Deserialize<T>(dataset);
            }

            return null;
        }

        private TCollection GetAll<T, TCollection>(Uri collectionUri, int page, int pageSize = 10)
            where T : class
            where TCollection : Collection<T>, new()
        {
            var query = new SparqlParameterizedString(Resource.AsString("SparqlQueries.GetSourcesPage.rq"));
            query.SetUri("type", GetTypeUri(typeof(T)));
            query.SetUri("collection", collectionUri);
            query.SetLiteral("page", page);
            query.SetLiteral("limit", pageSize);
            query.SetLiteral("offset", (page - 1) * pageSize);
            var graph = (IGraph)this.queryProcessor.Value.ProcessQuery(this.parser.ParseFromString(query.ToString()));

            if (graph.Triples.Count > 0)
            {
                var dataset = StringWriter.Write(graph, new NTriplesWriter(NTriplesSyntax.Rdf11));

                return this.serializer.Deserialize<TCollection>(dataset);
            }

            return new TCollection();
        }
    }
}
