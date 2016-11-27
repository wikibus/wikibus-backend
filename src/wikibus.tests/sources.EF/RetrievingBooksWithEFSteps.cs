using System;
using FluentAssertions;
using TechTalk.SpecFlow;
using Wikibus.Sources;

namespace Wikibus.Tests.sources.EF
{
    [Binding, Scope(Tag = "EF"), Scope(Tag = "Book")]
    public class RetrievingBooksWithEfSteps
    {
        private readonly EntityFrameworkSourceTestContext context;

        public RetrievingBooksWithEfSteps(EntityFrameworkSourceTestContext context)
        {
            this.context = context;
        }

        [When(@"getting Book <(.*)>")]
        public void WhenGettingBook(string bookId)
        {
            context.Source = context.Repository.GetBook(new Uri(bookId));
        }

        [Then(@"Author should be '(.*)'")]
        public void ThenAuthorShouldBe(string author)
        {
            context.Source.As<Book>().Author.Name.Should().Be(author);
        }
    }
}