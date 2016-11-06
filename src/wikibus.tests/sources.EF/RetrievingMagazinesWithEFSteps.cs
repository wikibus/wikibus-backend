using System;
using FluentAssertions;
using TechTalk.SpecFlow;
using wikibus.sources;

namespace wikibus.tests.sources.EF
{
    [Binding, Scope(Tag = "EF"), Scope(Tag = "MagazineIssue")]
    public class RetrievingMagazinesWithEFSteps
    {
        private readonly EntitiFrameworkSourceTestContext _context;

        public RetrievingMagazinesWithEFSteps(EntitiFrameworkSourceTestContext context)
        {
            _context = context;
        }

        [When(@"Getting issue <(.*)>")]
        public void WhenGettingBook(string issueId)
        {
            _context.Source = _context.Repository.GetIssue(new Uri(issueId));
        }

        [Then(@"Magazine is <(.*)>")]
        public void ThenMagazineIs(string magazineId)
        {
            _context.Source.As<Issue>().Magazine.Id.Should().Be(new Uri(magazineId));
        }
    }
}