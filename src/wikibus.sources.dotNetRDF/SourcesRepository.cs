﻿using System;
using System.Threading.Tasks;
using Argolis.Hydra.Resources;
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
        public async Task<Magazine> GetMagazine(Uri identifier)
        {
            return this.Get<Magazine>(identifier);
        }

        /// <inheritdoc />
        public async Task<Brochure> GetBrochure(Uri identifier)
        {
            return this.Get<Brochure>(identifier);
        }

        /// <inheritdoc />
        public async Task<Book> GetBook(Uri identifier)
        {
            return this.Get<Book>(identifier);
        }

        /// <inheritdoc />
        public Task<SearchableCollection<Book>> GetBooks(Uri identifier, BookFilters filters, int page, int pageSize = 10)
        {
            return this.GetAll<Book, SearchableCollection<Book>>(identifier, page, pageSize);
        }

        /// <inheritdoc />
        public Task<SearchableCollection<Brochure>> GetBrochures(Uri identifier, BrochureFilters filters, int page, int pageSize = 10)
        {
            return this.GetAll<Brochure, SearchableCollection<Brochure>>(identifier, page, pageSize);
        }

        /// <inheritdoc />
        public Task<SearchableCollection<Magazine>> GetMagazines(Uri identifier, MagazineFilters filters, int page, int pageSize = 10)
        {
            return this.GetAll<Magazine, SearchableCollection<Magazine>>(identifier, page, pageSize);
        }

        /// <inheritdoc />
        public async Task<Collection<Issue>> GetMagazineIssues(Uri identifier)
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
        public async Task<Issue> GetIssue(Uri identifier)
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

        private async Task<TCollection> GetAll<T, TCollection>(Uri collectionUri, int page, int pageSize = 10)
            where T : class
            where TCollection : SearchableCollection<T>, new()
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
