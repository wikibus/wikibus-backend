using System;
using System.Data;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using TCode.r2rml4net;
using TCode.r2rml4net.Log;
using TechTalk.SpecFlow;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace wikibus.tests.Mappings
{
   [Binding]
   public class MappingSourcesSteps
   {
      private readonly IDbConnection _conn;
      private readonly IR2RMLProcessor _rmlProc;
      private ITripleStore _result;

      public MappingSourcesSteps()
      {
         _conn = A.Fake<IDbConnection>(mock => mock.Strict());
         A.CallTo(() => _conn.State).Returns(ConnectionState.Open);
         _rmlProc = new W3CR2RMLProcessor(_conn) { Log = new TextWriterLog(Console.Out) };
      }

      [Given(@"table '(.*)' with data:")]
      public void GivenTableWithData(string tableName, Table table)
      {
         A.CallTo(() => _conn.CreateCommand()).Returns(new FakeCommand(tableName, table));
      }

      [When(@"retrieve all triples")]
      public void WhenRetrieveAllTriples()
      {
         _result = _rmlProc.GenerateTriples(CreateRdbMappings());
      }

      [Then(@"resulting dataset should match query:")]
      public void ThenResultingShouldBeEqualTo(string query)
      {
         ISparqlQueryProcessor processor = new LeviathanQueryProcessor((IInMemoryQueryableStore)_result);

         var queryResult = (SparqlResultSet)processor.ProcessQuery(new SparqlQueryParser().ParseFromString(query));

         Assert.That(queryResult.Result);
      }

      [Then(@"resulting dataset should contain '(\d*)' triples")]
      public void ThenResultingDatasetShouldContainTriples(int expectedCount)
      {
         Assert.That(_result.Triples.Count(), Is.EqualTo(expectedCount));
      }

      private IR2RML CreateRdbMappings()
      {
         var rml = new FluentR2RML();

         var brochureMap = rml.CreateTriplesMapFromR2RMLView(@"SELECT [Id]
      ,CASE [SourceType]
        WHEN 'folder' THEN 'Brochure'
        WHEN 'book' THEN 'Book'
        WHEN 'file' THEN 'File'
        WHEN 'magissue' THEN 'Issue'
       END as [Type]
      ,[Language]
      ,[Language2]
      ,[Pages]
      ,[Year]
      ,[Month]
      ,[Day]
      ,[Notes]
      ,[FolderCode]
      ,[FolderName]
      ,[BookTitle]
      ,[BookAuthor]
      ,[BookISBN]
      ,[MagIssueMagazine]
      ,[MagIssueNumber]
      ,[FileMimeType]
      ,[Url]
      ,[FileName]
  FROM [Sources].[Source]");
         brochureMap.SubjectMap.IsTemplateValued("http://wikibus.org/brochure/{Id}");

         var titleMap = brochureMap.CreatePropertyObjectMap();
         titleMap.CreatePredicateMap().IsConstantValued(new Uri("http://purl.org/dc/terms/title"));
         titleMap.CreateObjectMap().IsColumnValued("FolderName");

         var typeMap = brochureMap.CreatePropertyObjectMap();
         typeMap.CreatePredicateMap().IsConstantValued(new Uri(RdfSpecsHelper.RdfType));
         typeMap.CreateObjectMap().IsTemplateValued("http://wikibus.org/ontology#{Type}");

         return rml;
      }
   }
}
