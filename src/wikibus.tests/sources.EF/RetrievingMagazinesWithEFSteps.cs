using System;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;
using Wikibus.Sources;

namespace Wikibus.Tests.sources.EF
{
    [Binding, Scope(Tag = "EF"), Scope(Tag = "MagazineIssue")]
    public class RetrievingMagazinesWithEfSteps
    {
        private readonly EntityFrameworkSourceTestContext context;

        public RetrievingMagazinesWithEfSteps(EntityFrameworkSourceTestContext context)
        {
            this.context = context;
        }

        [When(@"Getting issue <(.*)>")]
        public async Task WhenGettingBook(string issueId)
        {
            context.Source = await context.Repository.GetIssue(new Uri(issueId));
        }

        [Then(@"Magazine is <(.*)>")]
        public void ThenMagazineIs(string magazineId)
        {
            context.Source.As<Issue>().Magazine.Id.Should().Be(new Uri(magazineId));
        }
    }
}