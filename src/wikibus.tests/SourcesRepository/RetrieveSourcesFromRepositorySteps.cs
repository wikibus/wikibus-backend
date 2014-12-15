using System;
using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using VDS.RDF;
using wikibus.sources;

namespace wikibus.tests.SourcesRepository
{
    [Binding]
    public class RetrieveSourcesFromRepositorySteps
    {
        private readonly RepositoryScenarioContext _context;

        public RetrieveSourcesFromRepositorySteps(RepositoryScenarioContext context)
        {
            _context = context;
        }

        [Given(@"RDF data:")]
        public void GivenRdfFrom(string datasetToLoad)
        {
            _context.Store.LoadFromString(datasetToLoad);
        }

        [When(@"brochure <(.*)> is fetched")]
        public void WhenBrochureIsFetched(string uri)
        {
            ScenarioContext.Current.Set(_context.Repository.Get<Brochure>(new Uri(uri)), "Model");
        }

        [When(@"book <(.*)> is fetched")]
        public void WhenBookIsFetched(string uri)
        {
            ScenarioContext.Current.Set(_context.Repository.Get<Book>(new Uri(uri)), "Model");
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

        [Then(@"'(.*)' should have string property '(.*)' equal to '(.*)'")]
        public void ThenShouldHaveStringPropertyEqualTo(string objName, string propName, string expectedValue)
        {
            var model = ScenarioContext.Current[objName];
            object value = ImpromptuInterface.Impromptu.InvokeGet(model, propName);

            value.Should().Be(expectedValue);
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
