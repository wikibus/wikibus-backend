using System;
using JsonLD.Core;
using Newtonsoft.Json.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace wikibus.sources.dotNetRDF
{
    public class SourcesRepository : ISourcesRepository
    {
        private readonly ISparqlQueryProcessor _queryProcessor;
        private readonly SparqlQueryParser _parser = new SparqlQueryParser();

        public SourcesRepository(ISparqlQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public T Get<T>(Uri uri) where T : Source
        {
            var query = new SparqlParameterizedString("CONSTRUCT { @s ?p ?o } WHERE { @s ?p ?o . @s a <http://wikibus.org/ontology#Brochure> . }");
            query.SetUri("s", uri);
            var triples = (IGraph)_queryProcessor.ProcessQuery(_parser.ParseFromString(query.ToString()));

            var dataset = System.Text.RegularExpressions.Regex.Unescape(StringWriter.Write(triples, new NTriplesWriter()));
            var result = JsonLdProcessor.FromRDF(dataset);
            result = JsonLdProcessor.Compact(result, JObject.Parse("{ 'title': 'http://purl.org/dc/terms/title' }"), new JsonLdOptions("http://wikibus.org/"));

            return result.ToObject<T>();
        }
    }
}