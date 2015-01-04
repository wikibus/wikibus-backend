using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using NDbUnit.Core;
using NUnit.Framework;
using TCode.r2rml4net;
using TCode.r2rml4net.Log;
using TechTalk.SpecFlow;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;
using wikibus.sources.dotNetRDF;
using wikibus.sources.dotNetRDF.Mapping;
using StringWriter = VDS.RDF.Writing.StringWriter;

namespace wikibus.tests.Mappings
{
    [Binding]
    public class MappingSourcesSteps : IDisposable
    {
        private readonly IR2RMLProcessor _rmlProc;
        private readonly INDbUnitTest _database;
        private readonly SqlConnection _sqlConnection;
        private ITripleStore _result;

        public MappingSourcesSteps()
        {
            _sqlConnection = new SqlConnection(Database.TestConnectionString);
            _database = Database.Initialize(_sqlConnection);
            _rmlProc = new W3CR2RMLProcessor(_sqlConnection)
            {
                Log = new TextWriterLog(Console.Out)
            };
        }

        [Given(@"table (.*) with data:")]
        public void GivenTableWithData(string tableName, Table table)
        {
            var datasetFile = Path.GetTempFileName();
            DataSet ds = table.ToDataSet(tableName);
            ds.WriteXml(datasetFile);
            _database.AppendXml(datasetFile);
        }

        [When(@"retrieve all triples")]
        public void WhenRetrieveAllTriples()
        {
            _database.PerformDbOperation(DbOperationFlag.Insert);
            _result = _rmlProc.GenerateTriples(new WikibusR2RML(new TestConfiguration()));
            _result.SaveToFile("out.trig");
        }

        [Then(@"resulting dataset should match query:")]
        public void ThenResultingShouldMatchQuery(string query)
        {
            var querySuccess = ExecuteAsk(query);
            Assert.That(querySuccess, "Actual triples were: {0}", StringWriter.Write(_result, new TriGWriter()));
        }

        [Then(@"resulting dataset should not match query:")]
        public void ThenResultingDatasetShouldNotMatchQuery(string query)
        {
            var querySuccess = ExecuteAsk(query);
            Assert.That(querySuccess, Is.False, "Actual triples were: {0}", StringWriter.Write(_result, new TriGWriter()));
        }

        [Then(@"resulting dataset should contain '(\d*)' triples")]
        public void ThenResultingDatasetShouldContainTriples(int expectedCount)
        {
            Assert.That(
                _result.Triples.Count(),
                Is.EqualTo(expectedCount),
                "Actual triples were: {0}",
                StringWriter.Write(_result, new TriGWriter()));
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        private bool ExecuteAsk(string query)
        {
            ISparqlQueryProcessor processor = new LeviathanQueryProcessor((IInMemoryQueryableStore)_result);

            var queryResult = (SparqlResultSet)processor.ProcessQuery(new SparqlQueryParser().ParseFromString(query));

            return queryResult.Result;
        }
    }
}
