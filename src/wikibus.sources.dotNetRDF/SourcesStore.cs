using System;
using System.Data;
using Resourcer;
using TCode.r2rml4net;
using VDS.RDF;
using wikibus.common;
using wikibus.common.Vocabularies;
using wikibus.sources.dotNetRDF.Mapping;

namespace wikibus.sources.dotNetRDF
{
    /// <summary>
    /// Store, which loads mapped triples from Sources SQL database
    /// </summary>
    public class SourcesStore : TripleStore
    {
        private static readonly string InitCollectionQuery = Resource.AsString("SparqlQueries.InitCollection.rq");
        private static readonly string InitIssuesCollectionQuery = Resource.AsString("SparqlQueries.InitIssueCollections.rq");

        private readonly IWikibusConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesStore"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="config">The configuration</param>
        public SourcesStore(IDbConnection connection, IWikibusConfiguration config)
        {
            _config = config;
            var processor = new W3CR2RMLProcessor(connection);

            processor.GenerateTriples(new WikibusR2RML(config), this);
        }

        /// <summary>
        /// Initializes the store with collections etc.
        /// </summary>
        public void Initialize()
        {
            ExecuteUpdate(InitIssuesCollectionQuery);
            ExecuteUpdate(GetInitCollectionQuery("books", Wbo.Book, "book"));
            ExecuteUpdate(GetInitCollectionQuery("brochures", Wbo.Brochure, "folder"));
            ExecuteUpdate(GetInitCollectionQuery("magazines", Wbo.Magazine, "magazine"));
        }

        private string GetInitCollectionQuery(string collectionUri, string elementType, string pattern)
        {
            var query = new VDS.RDF.Query.SparqlParameterizedString(InitCollectionQuery);
            query.SetUri("collection", new Uri(new Uri(_config.BaseResourceNamespace), collectionUri));
            query.SetUri("elementType", new Uri(elementType));
            query.SetLiteral("pattern", pattern);

            return query.ToString();
        }
    }
}
