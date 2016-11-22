using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using TechTalk.SpecFlow;
using VDS.RDF;
using Wikibus.Common.Vocabularies;
using Wikibus.Sources;

namespace wikibus.tests.SourcesRepository
{
    [Binding]
    public class RetrieveSourcesFromRepositorySteps
    {
        private readonly RepositoryScenarioContext context;

        public RetrieveSourcesFromRepositorySteps(RepositoryScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"RDF data:")]
        public void GivenRdfFrom(string datasetToLoad)
        {
            context.Store.LoadFromString(datasetToLoad);
        }

        [Given(@"(.*) books")]
        public void GivenBooks(int count)
        {
            foreach (var i in Enumerable.Range(1, count))
            {
                const string format = @"prefix hydra: <http://www.w3.org/ns/hydra/core#>
INSERT DATA
{{
<urn:book:collection> <http://xmlns.com/foaf/0.1/primaryTopic> <http://wikibus.org/books> .
<http://data.wikibus.org/graph/r2rml/{0}> <http://xmlns.com/foaf/0.1/primaryTopic> <http://wikibus.org/book/{0}> .

GRAPH <http://data.wikibus.org/graph/r2rml/{0}>
{{
    <http://wikibus.org/book/{0}> a <{1}>
}}

GRAPH <urn:book:collection>
{{
    <http://wikibus.org/books> hydra:member <http://wikibus.org/book/{0}>
}}
}}";
                var update = string.Format(format, i, Wbo.Book);
                context.Store.ExecuteUpdate(update);
            }
        }

        [When(@"brochure <(.*)> is fetched")]
        public void WhenBrochureIsFetched(string uri)
        {
            ScenarioContext.Current.Set(context.Repository.GetBrochure(new Uri(uri)), "Model");
        }

        [When(@"book <(.*)> is fetched")]
        public void WhenBookIsFetched(string uri)
        {
            ScenarioContext.Current.Set(context.Repository.GetBook(new Uri(uri)), "Model");
        }

        [When(@"magazine <(.*)> is fetched")]
        public void WhenMagazineIsFetched(string uri)
        {
            ScenarioContext.Current.Set(context.Repository.GetMagazine(new Uri(uri)), "Model");
        }

        [When(@"issue <(.*)> is fetched")]
        public void WhenIssueIsFetched(string uri)
        {
            ScenarioContext.Current.Set(context.Repository.GetIssue(new Uri(uri)), "Model");
        }

        [Then(@"'(.*)' should be string equal to '(.*)'")]
        public void PropertyShouldBeStrigWithValueEqual(string propName, string expectedValue)
        {
            ScenarioContext.Current["Model"].AssertPropertyValue(propName, expectedValue);
        }

        [Then(@"'(.*)' should be integer equal to '(.*)'")]
        public void PropertyShouldBeIntegerWithValueEqual(string propName, string expectedValue)
        {
            ScenarioContext.Current["Model"].AssertPropertyValue(propName, int.Parse(expectedValue));
        }

        [Then(@"'(.*)' should be DateTime equal to '(.*)'")]
        public void PropertyShouldBeDateTimeWithValueEqual(string propName, string expectedValue)
        {
            ScenarioContext.Current["Model"].AssertPropertyValue(propName, DateTime.Parse(expectedValue));
        }

        [Then(@"Languages should contain '(.*)'")]
        public void ThenLanguageShouldContain(string langCode)
        {
            var model = (Source)ScenarioContext.Current["Model"];

            model.Languages.Should().Contain(new Language(langCode));
        }

        [Then(@"'(.*)' should be null")]
        public void ThenShouldBeNull(string propName)
        {
            var model = ScenarioContext.Current["Model"];
            object value = ImpromptuInterface.Impromptu.InvokeGet(model, propName);

            value.Should().Be(model.GetType().GetProperty(propName).PropertyType.GetDefaultValue());
        }

        [Then(@"'(.*)' should be not null")]
        public void ThenShouldBeNotNull(string propName)
        {
            var model = ScenarioContext.Current["Model"];
            object value = ImpromptuInterface.Impromptu.InvokeGet(model, propName);

            value.Should().NotBe(model.GetType().GetProperty(propName).PropertyType.GetDefaultValue());

            ScenarioContext.Current.Set(value, propName);
        }

        [Then(@"'(.*)' should be empty")]
        public void ThenLanguageShouldBeEmpty(string propName)
        {
            var model = ScenarioContext.Current["Model"];
            var value = ImpromptuInterface.Impromptu.InvokeGet(model, propName);

            ((IEnumerable)value).Should().BeEmpty();
        }
    }
}
