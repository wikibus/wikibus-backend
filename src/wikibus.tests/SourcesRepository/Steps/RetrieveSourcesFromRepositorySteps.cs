using System;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using VDS.RDF;
using wikibus.sources;

namespace wikibus.tests.SourcesRepository.Steps
{
    [Binding]
    public class RetrieveSourcesFromRepositorySteps
    {
        private readonly TripleStore _store = new TripleStore();
        private Brochure _brochure;

        [Given(@"Rdf from '(.*)'")]
        public void GivenRdfFrom(string datasetToLoad)
        {
            _store.LoadFromEmbeddedResource(string.Format("wikibus.tests.Graphs.{0}, wikibus.tests", datasetToLoad));
        }

        [Given(@"@context from '(.*)'")]
        public void GivenContextFrom(string contextFile)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"brochure <(.*)> is fetched")]
        public void WhenBrochureIsFetched(string uri)
        {
            ISourcesRepository repository = new sources.dotNetRDF.SourcesRepository();

            _brochure = repository.Get<Brochure>(new Uri(uri));
        }

        [Then(@"the brochure should have title '(.*)'")]
        public void ThenTheBrochureShouldHaveName(string title)
        {
            _brochure.Title.Should().Be(title);
        }
    }
}
