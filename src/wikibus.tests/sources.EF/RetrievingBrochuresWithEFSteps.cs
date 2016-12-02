using System;
using TechTalk.SpecFlow;

namespace Wikibus.Tests.sources.EF
{
    [Binding, Scope(Tag = "EF"), Scope(Tag = "Brochure")]
    public class RetrievingBrochuresWithEfSteps
    {
        private readonly EntityFrameworkSourceTestContext context;

        public RetrievingBrochuresWithEfSteps(EntityFrameworkSourceTestContext context)
        {
            this.context = context;
        }

        [When(@"getting Brochure <(.*)>")]
        public void WhenGettingBrochure(string brochureId)
        {
            context.Source = context.Repository.GetBrochure(new Uri(brochureId)).Result;
        }
    }
}
