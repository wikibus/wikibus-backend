using System;
using System.Collections;
using System.Globalization;
using FluentAssertions;
using JsonLD.Entities;
using NUnit.Framework;
using TechTalk.SpecFlow;
using VDS.RDF;
using VDS.RDF.Query;
using wikibus.sources;
using wikibus.sources.dotNetRDF;

namespace wikibus.tests.SourcesRepository.Steps
{
    [Binding]
    public class RetrieveSourcesFromRepositorySteps
    {
        private readonly TripleStore _store = new TripleStore();
        private readonly IEntitySerializer _serializer;
        private ISparqlQueryProcessor _queryProcessor;

        public RetrieveSourcesFromRepositorySteps()
        {
            var contextProvider = new StaticContextProvider();
            contextProvider.SetupSourcesContexts();
            _serializer = new EntitySerializer(contextProvider);
        }

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
            ISourcesRepository repository = new sources.dotNetRDF.SourcesRepository(_queryProcessor, _serializer);

            ScenarioContext.Current.Set(repository.Get<Brochure>(new Uri(uri)), "Model");
        }

        [Then(@"'(.*)' should be string equal to '(.*)'")]
        public void PropertyShouldBeStrigWithValueEqual(string propName, string expectedValue)
        {
            AssertPropertyValue(propName, expectedValue);
        }

        [Then(@"'(.*)' should be integer equal to '(.*)'")]
        public void PropertyShouldBeIntegerWithValueEqual(string propName, string expectedValue)
        {
            AssertPropertyValue(propName, int.Parse(expectedValue));
        }

        [Then(@"'(.*)' should be DateTime equal to '(.*)'")]
        public void PropertyShouldBeDateTimeWithValueEqual(string propName, string expectedValue)
        {
            AssertPropertyValue(propName, DateTime.Parse(expectedValue));
        }

        [Then(@"Languages should contain '(.*)'")]
        public void ThenLanguageShouldContain(string langCode)
        {
            var model = (Source)ScenarioContext.Current["Model"];

            model.Languages.Should().Contain(CultureInfo.GetCultureInfo(langCode));
        }

        [Then(@"'(.*)' should be null")]
        public void ThenShouldBeNull(string propName)
        {
            var model = ScenarioContext.Current["Model"];
            object value = ImpromptuInterface.Impromptu.InvokeGet(model, propName);

            value.Should().Be(model.GetType().GetProperty(propName).PropertyType.GetDefaultValue());
        }

        [Then(@"'(.*)' should be empty")]
        public void ThenLanguageShouldBeEmpty(string propName)
        {
            var model = ScenarioContext.Current["Model"];
            var value = ImpromptuInterface.Impromptu.InvokeGet(model, propName);

            ((IEnumerable)value).Should().BeEmpty();
        }

        private void AssertPropertyValue(string propName, object expected)
        {
            var model = ScenarioContext.Current["Model"];
            var value = ImpromptuInterface.Impromptu.InvokeGet(model, propName);

            Assert.That(value, Is.EqualTo(expected));
        }
    }
}
