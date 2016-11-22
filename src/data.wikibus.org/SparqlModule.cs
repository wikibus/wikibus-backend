using System;
using System.IO;
using Nancy;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace Data.Wikibus.Org
{
    /// <summary>
    /// Handles SPARQL queries
    /// </summary>
    public class SparqlModule : NancyModule
    {
        private readonly Lazy<ISparqlQueryProcessor> queryProcessor;
        private readonly SparqlQueryParser parser = new SparqlQueryParser();
        private readonly IRdfWriter writer = new Notation3Writer();
        private readonly ISparqlResultsWriter sparqlWriter = new SparqlXmlWriter();

        /// <summary>
        /// Initializes a new instance of the <see cref="SparqlModule"/> class.
        /// </summary>
        /// <param name="queryProcessor">The query processor.</param>
        public SparqlModule(Lazy<ISparqlQueryProcessor> queryProcessor)
            : base("sparql")
        {
            this.queryProcessor = queryProcessor;

            this.Get("/", request => this.ProcessQuery());
        }

        private object ProcessQuery()
        {
            string query = this.Request.Query.query;

            var result = this.queryProcessor.Value.ProcessQuery(this.parser.ParseFromString(query));

            if (result is IGraph)
            {
                return new Response
                {
                    Contents = stream => this.writer.Save((IGraph)result, new StreamWriter(stream))
                };
            }

            if (result is SparqlResultSet)
            {
                return new Response
                {
                    Contents = stream => this.sparqlWriter.Save((SparqlResultSet)result, new StreamWriter(stream)),
                    ContentType = "application/sparql-results+xml"
                };
            }

            return 400;
        }
    }
}
