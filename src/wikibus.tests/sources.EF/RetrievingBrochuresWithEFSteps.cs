using System;
using TechTalk.SpecFlow;

namespace wikibus.tests.sources.EF
{
    [Binding, Scope(Tag = "EF"), Scope(Tag = "Brochure")]
    public class RetrievingBrochuresWithEFSteps
    {
        private readonly EntitiFrameworkSourceTestContext _context;

        public RetrievingBrochuresWithEFSteps(EntitiFrameworkSourceTestContext context)
        {
            _context = context;
        }

        [When(@"getting Brochure <(.*)>")]
        public void WhenGettingBrochure(string brochureId)
        {
            _context.Source = _context.Repository.GetBrochure(new Uri(brochureId));
        }
    }
}
