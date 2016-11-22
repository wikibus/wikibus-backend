using System;
using TechTalk.SpecFlow;
using Wikibus.Sources.Filters;

namespace Wikibus.Tests.sources.dotNetRDF.SourcesRepository
{
    [Binding]
    public class RetrieveSourcesCollectionsFromRepositorySteps
    {
        private readonly RepositoryScenarioContext context;

        public RetrieveSourcesCollectionsFromRepositorySteps(RepositoryScenarioContext context)
        {
            this.context = context;
        }

        [When(@"page (.*) of (.*) is fetched")]
        public void WhenPageOfBooksIsFetched(int expectedPageIndex, string uri)
        {
            var books = context.Repository.GetBooks(new Uri(uri), new BookFilters(), expectedPageIndex);
            ScenarioContext.Current.Set(books, "Model");
        }

        [Then(@"'(.*)' should be (\d*)")]
        public void ThenShouldBe(string propName, int expectedValue)
        {
            ScenarioContext.Current["Model"]
                .AssertPropertyValue(propName, expectedValue);
        }

        [Then(@"'(.*)' should be Uri '(.*)'")]
        public void ThenShouldBeUri(string propName, string expectedUri)
        {
            ScenarioContext.Current["Model"]
                .AssertPropertyValue(propName, new Uri(expectedUri));
        }
    }
}
