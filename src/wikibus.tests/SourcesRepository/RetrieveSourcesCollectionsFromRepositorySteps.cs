using System;
using TechTalk.SpecFlow;

namespace wikibus.tests.SourcesRepository
{
    [Binding]
    public class RetrieveSourcesCollectionsFromRepositorySteps
    {
        private readonly RepositoryScenarioContext _context;

        public RetrieveSourcesCollectionsFromRepositorySteps(RepositoryScenarioContext context)
        {
            _context = context;
        }

        [When(@"page (.*) of books is fetched")]
        public void WhenPageOfBooksIsFetched(int expectedPageIndex)
        {
            var books = _context.Repository.GetBooks(expectedPageIndex);
            ScenarioContext.Current.Set(books, "Model");
        }

        [Then(@"'(.*)' should be (.*)")]
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
