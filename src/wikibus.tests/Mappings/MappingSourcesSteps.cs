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
using Wikibus.Sources.DotNetRDF.Mapping;
using StringWriter = VDS.RDF.Writing.StringWriter;

namespace Wikibus.Tests.Mappings
{
    [Binding]
    public class MappingSourcesSteps : IDisposable
    {
        private readonly IR2RMLProcessor rmlProc;
        private readonly INDbUnitTest database;
        private readonly SqlConnection sqlConnection;
        private ITripleStore result;

        public MappingSourcesSteps()
        {
            sqlConnection = new SqlConnection(Database.TestConnectionString);
            database = Database.Initialize(sqlConnection);
            rmlProc = new W3CR2RMLProcessor(sqlConnection)
            {
                Log = new TextWriterLog(Console.Out)
            };
        }

        [Given(@"table (.*) with data:"), Scope(Tag = "SQL")]
        public void GivenTableWithData(string tableName, Table table)
        {
            var datasetFile = Path.GetTempFileName();
            DataSet ds = table.ToDataSet(tableName);
            ds.WriteXml(datasetFile);
            database.AppendXml(datasetFile);
        }

        [Given("data is inserted"), Scope(Tag = "SQL")]
        public void GiveDataInserted()
        {
            database.PerformDbOperation(DbOperationFlag.Insert);
        }

        [When(@"retrieve all triples"), Scope(Tag = "RML")]
        public void WhenRetrieveAllTriples()
        {
            database.PerformDbOperation(DbOperationFlag.Insert);
            result = rmlProc.GenerateTriples(new WikibusR2RML(new TestConfiguration()));
            result.SaveToFile("out.trig");
        }

        [Then(@"resulting dataset should match query:"), Scope(Tag = "RML")]
        public void ThenResultingShouldMatchQuery(string query)
        {
            var querySuccess = ExecuteAsk(query);
            Assert.That(querySuccess, "Actual triples were: {0}", StringWriter.Write(result, new TriGWriter()));
        }

        [Then(@"resulting dataset should not match query:"), Scope(Tag = "RML")]
        public void ThenResultingDatasetShouldNotMatchQuery(string query)
        {
            var querySuccess = ExecuteAsk(query);
            Assert.That(querySuccess, Is.False, "Actual triples were: {0}", StringWriter.Write(result, new TriGWriter()));
        }

        [Then(@"resulting dataset should contain '(\d*)' triples"), Scope(Tag = "RML")]
        public void ThenResultingDatasetShouldContainTriples(int expectedCount)
        {
            Assert.That(
                result.Triples.Count(),
                Is.EqualTo(expectedCount),
                "Actual triples were: {0}",
                StringWriter.Write(result, new TriGWriter()));
        }

        public void Dispose()
        {
            sqlConnection.Dispose();
        }

        private bool ExecuteAsk(string query)
        {
            ISparqlQueryProcessor processor = new LeviathanQueryProcessor((IInMemoryQueryableStore)result);

            var queryResult = (SparqlResultSet)processor.ProcessQuery(new SparqlQueryParser().ParseFromString(query));

            return queryResult.Result;
        }
    }
}
