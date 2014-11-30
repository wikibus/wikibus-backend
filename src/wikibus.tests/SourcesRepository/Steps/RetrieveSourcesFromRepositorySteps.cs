using System;
using FakeItEasy;
using FluentAssertions;
using JsonLD.Entities;
using TechTalk.SpecFlow;
using VDS.RDF;
using VDS.RDF.Query;
using wikibus.sources;

namespace wikibus.tests.SourcesRepository.Steps
{
    [Binding]
    public class RetrieveSourcesFromRepositorySteps
    {
        private readonly TripleStore _store = new TripleStore();
        private ISparqlQueryProcessor _queryProcessor;

        [Given(@"In-memory query processor")]
        public void GivenInMemoryQueryProcessor()
        {
            _queryProcessor = new LeviathanQueryProcessor(_store);
        }

        [Given(@"RDF data:")]
        public void GivenRdfFrom(string datasetToLoad)
        {
            _store.LoadFromString(datasetToLoad);
        }

        [When(@"brochure <(.*)> is fetched")]
        public void WhenBrochureIsFetched(string uri)
        {
            ISourcesRepository repository = new sources.dotNetRDF.SourcesRepository(_queryProcessor, A.Fake<IEntitySerializer>());

            ScenarioContext.Current.Set(repository.Get<Brochure>(new Uri(uri)));
        }

        [Then(@"the brochure should have title '(.*)'")]
        public void ThenTheBrochureShouldHaveName(string title)
        {
            ScenarioContext.Current.Get<Brochure>().Title.Should().Be(title);
        }
    }
}
