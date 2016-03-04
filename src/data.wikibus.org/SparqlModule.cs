using System;
using System.IO;
using Nancy;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace data.wikibus.org
{
    /// <summary>
    /// Handles SPARQL queries
    /// </summary>
    public class SparqlModule : NancyModule
    {
        private readonly Lazy<ISparqlQueryProcessor> _queryProcessor;
        private readonly SparqlQueryParser _parser = new SparqlQueryParser();
        private readonly IRdfWriter _writer = new Notation3Writer();
        private readonly ISparqlResultsWriter _sparqlWriter = new SparqlXmlWriter();

        /// <summary>
        /// Initializes a new instance of the <see cref="SparqlModule"/> class.
        /// </summary>
        /// <param name="queryProcessor">The query processor.</param>
        public SparqlModule(Lazy<ISparqlQueryProcessor> queryProcessor) : base("sparql")
        {
            _queryProcessor = queryProcessor;

            Get["/"] = request => ProcessQuery();
        }

        private object ProcessQuery()
        {
            string query = Request.Query.query;

            var result = _queryProcessor.Value.ProcessQuery(_parser.ParseFromString(query));

            if (result is IGraph)
            {
                return new Response
                {
                    Contents = stream => _writer.Save((IGraph)result, new StreamWriter(stream))
                };
            }

            if (result is SparqlResultSet)
            {
                return new Response
                {
                    Contents = stream => _sparqlWriter.Save((SparqlResultSet)result, new StreamWriter(stream)),
                    ContentType = "application/sparql-results+xml"
                };
            }

            return 400;
        }
    }
}
