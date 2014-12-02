using System;
using FakeItEasy;
using NUnit.Framework;
using Slp.r2rml4net.Storage;
using Slp.r2rml4net.Storage.Query;
using Slp.r2rml4net.Storage.Sql;
using Slp.r2rml4net.Storage.Sql.Algebra;
using Slp.r2rml4net.Storage.Sql.Vendor;
using TCode.r2rml4net;
using TechTalk.SpecFlow;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Storage;

namespace wikibus.tests.Mappings
{
    [Binding]
    public class MappingSourcesSteps
    {
        private readonly IQueryableStorage _storage;
        private readonly ISqlDb _sqlDb;
        private IGraph _result;

        public MappingSourcesSteps()
        {
            _sqlDb = A.Fake<BaseSqlDb>(mock => mock.Strict());

            _storage = new R2RMLStorage(CreateRdbMappings(), _sqlDb);
        }

        [Given(@"source table with data:")]
        public void GivenSourceTableWithData(Table table)
        {
            IQueryResultReader resultWrapper = table.ToStaticDataReader();

            A.CallTo(() => _sqlDb.ExecuteQuery(A<string>.Ignored, A<QueryContext>.Ignored))
             .Returns(resultWrapper);
        }

        [When(@"retrieve all triples")]
        public void WhenRetrieveAllTriples()
        {
            _result = (IGraph)_storage.Query("construct { ?s ?p ?o } where { ?s ?p ?o }");
        }

        [Then(@"resulting graph should be equal to:")]
        public void ThenResultingGraphShouldBeEqualTo(string expectedGraph)
        {
            var expected = new Graph();
            expected.LoadFromString(expectedGraph);

            Assert.That(_result.Difference(expected).AreEqual);
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
