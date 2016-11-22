using System;
using TechTalk.SpecFlow;

namespace wikibus.tests.sources.EF
{
    [Binding, Scope(Tag = "EF"), Scope(Tag = "Brochure")]
    public class RetrievingBrochuresWithEfSteps
    {
        private readonly EntitiFrameworkSourceTestContext context;

        public RetrievingBrochuresWithEfSteps(EntitiFrameworkSourceTestContext context)
        {
            this.context = context;
        }

        [When(@"getting Brochure <(.*)>")]
        public void WhenGettingBrochure(string brochureId)
        {
            context.Source = context.Repository.GetBrochure(new Uri(brochureId));
        }
    }
}
