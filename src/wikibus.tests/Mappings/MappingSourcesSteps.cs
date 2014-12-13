using System;
using System.Configuration;
using System.Data;
using System.Linq;
using FakeItEasy;
using NDbUnit.Core;
using NUnit.Framework;
using TCode.r2rml4net;
using TCode.r2rml4net.Log;
using TechTalk.SpecFlow;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using wikibus.sources.dotNetRDF;

namespace wikibus.tests.Mappings
{
    [Binding]
    public class MappingSourcesSteps
    {
        private readonly IDbConnection _conn;
        private readonly IR2RMLProcessor _rmlProc;
        private readonly INDbUnitTest _database;
        private ITripleStore _result;

        public MappingSourcesSteps()
        {
            _conn = A.Fake<IDbConnection>(mock => mock.Strict());
            A.CallTo(() => _conn.State).Returns(ConnectionState.Open);
            _rmlProc = new W3CR2RMLProcessor(_conn) { Log = new TextWriterLog(Console.Out) };

            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
            _database = new NDbUnit.Core.SqlClient.SqlDbUnitTest(connectionString);
            _database.ReadXmlSchema(Resource.AsStream("Wikibus.xsd"));
            _database.PerformDbOperation(DbOperationFlag.DeleteAll);
        }

        [Given(@"table (.*) with data:")]
        public void GivenTableWithData(string tableName, Table table)
        {
            var ds = table.ToDataSet(tableName);
            ds.WriteXml("TestData.xml");
            _database.ReadXml("TestData.xml");
        }

        [When(@"retrieve all triples")]
        public void WhenRetrieveAllTriples()
        {
            _result = _rmlProc.GenerateTriples(new WikibusR2RML());
        }

        [Then(@"resulting dataset should match query:")]
        public void ThenResultingShouldMatchQuery(string query)
        {
            var querySuccess = ExecuteAsk(query);
            Assert.That(querySuccess);
        }

        [Then(@"resulting dataset should not match query:")]
        public void ThenResultingDatasetShouldNotMatchQuery(string query)
        {
            var querySuccess = ExecuteAsk(query);
            Assert.That(querySuccess, Is.False);
        }

        [Then(@"resulting dataset should contain '(\d*)' triples")]
        public void ThenResultingDatasetShouldContainTriples(int expectedCount)
        {
            Assert.That(_result.Triples.Count(), Is.EqualTo(expectedCount));
        }

        private bool ExecuteAsk(string query)
        {
            ISparqlQueryProcessor processor = new LeviathanQueryProcessor((IInMemoryQueryableStore)_result);

            var queryResult = (SparqlResultSet)processor.ProcessQuery(new SparqlQueryParser().ParseFromString(query));

            return queryResult.Result;
        }
    }
}
