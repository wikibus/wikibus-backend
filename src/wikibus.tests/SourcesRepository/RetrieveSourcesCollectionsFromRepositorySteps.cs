using System;
using TechTalk.SpecFlow;
using wikibus.sources;
using wikibus.sources.Hydra;

namespace wikibus.tests.SourcesRepository
{
    [Binding]
    public class RetrieveSourcesCollectionsFromRepositorySteps
    {
        private readonly RepositoryScenarioContext _context;
        private int _pageSize;

        public RetrieveSourcesCollectionsFromRepositorySteps(RepositoryScenarioContext context)
        {
            _context = context;
        }

        [Given(@"page size equal to (.*)")]
        public void GivenPageSizeEqualTo(int pageSize)
        {
            _pageSize = pageSize;
        }

        [When(@"page (.*) of books is fetched")]
        public void WhenPageOfBooksIsFetched(int expectedPageIndex)
        {
            var books = _context.Repository.GetAll<Book>(expectedPageIndex, _pageSize);
            ScenarioContext.Current.Set(books);
        }

        [Then(@"'(.*)' should be (.*)")]
        public void ThenShouldBe(string propName, int expectedValue)
        {
            ScenarioContext.Current.Get<PagedCollection<Book>>()
                .AssertPropertyValue(propName, expectedValue);
        }

        [Then(@"'(.*)' should be Uri '(.*)'")]
        public void ThenShouldBeUri(string propName, string expectedUri)
        {
            ScenarioContext.Current.Get<PagedCollection<Book>>()
                .AssertPropertyValue(propName, new Uri(expectedUri));
        }
    }
}
