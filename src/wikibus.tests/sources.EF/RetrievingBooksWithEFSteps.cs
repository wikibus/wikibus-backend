using System;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using wikibus.sources;

namespace wikibus.tests.sources.EF
{
    [Binding, Scope(Tag = "EF"), Scope(Tag = "Book")]
    public class RetrievingBooksWithEFSteps
    {
        private readonly EntitiFrameworkSourceTestContext _context;

        public RetrievingBooksWithEFSteps(EntitiFrameworkSourceTestContext context)
        {
            _context = context;
        }

        [When(@"getting Book <(.*)>")]
        public void WhenGettingBook(string bookId)
        {
            _context.Source = _context.Repository.GetBook(new Uri(bookId));
        }

        [Then(@"Author should be '(.*)'")]
        public void ThenAuthorShouldBe(string author)
        {
            _context.Source.As<Book>().Author.Name.Should().Be(author);
        }
    }
}