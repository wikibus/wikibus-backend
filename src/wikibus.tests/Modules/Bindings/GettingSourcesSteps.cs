using System;
using FakeItEasy;
using FluentAssertions;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using TechTalk.SpecFlow;
using wikibus.sources;

namespace wikibus.tests.Modules.Bindings
{
    [Binding]
    public class NancyTestingSteps
    {
        private readonly NancyDependencies _dep;
        private string _mimeType;

        public NancyTestingSteps(NancyDependencies dep)
        {
            _dep = dep;
        }

        [Given(@"Accept header is '(.*)'")]
        public void GivenAcceptHeaderIs(string acceptHeader)
        {
            _mimeType = acceptHeader;
        }

        [Given(@"brochure '(.*)' doesn't exist")]
        public void GivenBrochureDoesnTExist(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.Get<Brochure>(new Uri(resourceUri))).Returns(null);
        }

        [When(@"I GET resource '(.*)'")]
        public void WhenGETResourceWithAccept(string path)
        {
            var response = _dep.Browser.Get(path, context => context.Accept(new MediaRange(_mimeType)));
            ScenarioContext.Current.Set(response);
        }

        [Then(@"response should have status (.*)")]
        public void ThenResponseShouldHaveStatus(int statusCode)
        {
            ScenarioContext.Current.Get<BrowserResponse>().StatusCode
                .Should().Be((HttpStatusCode)statusCode);
        }
        
        [Then(@"brochure '(.*)' should have been retrieved")]
        public void ThenBrochureShouldHaveBeenRetrieved(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.Get<Brochure>(new Uri(resourceUri))).MustHaveHappened();
        }
    }
}
