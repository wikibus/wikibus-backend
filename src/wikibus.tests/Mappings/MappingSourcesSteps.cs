using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        private readonly IR2RMLProcessor _rmlProc;
        private readonly INDbUnitTest _database;
        private ITripleStore _result;

        public MappingSourcesSteps()
        {
            _database = Database.Initialize();
            _rmlProc = new W3CR2RMLProcessor(new SqlConnection(Database.TestConnectionString))
            {
                Log = new TextWriterLog(Console.Out)
            };
        }

        [Given(@"table (.*) with data:")]
        public void GivenTableWithData(string tableName, Table table)
        {
            DataSet ds = table.ToDataSet(tableName);
            ds.WriteXml("TestData.xml");
            _database.ReadXml("TestData.xml");
            _database.PerformDbOperation(DbOperationFlag.InsertIdentity);
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
