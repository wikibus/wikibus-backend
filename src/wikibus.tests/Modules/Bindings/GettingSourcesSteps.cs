using System;
using FakeItEasy;
using FluentAssertions;
using ImpromptuInterface;
using ImpromptuInterface.InvokeExt;
using Nancy;
using Nancy.RDF.Responses;
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
        private string _mimeType = RdfSerialization.Turtle.MediaType;

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
        public void GivenBrochureDoesntExist(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.Get<Brochure>(new Uri(resourceUri))).Returns(null);
        }

        [Given(@"existing brochure '(.*)'")]
        public void GivenExistingBrochure(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.Get<Brochure>(new Uri(resourceUri))).Returns(new Brochure());
        }

        [Given(@"existing book '(.*)'")]
        public void GivenExistingBook(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.Get<Book>(new Uri(resourceUri))).Returns(new Book());
        }

        [When(@"I GET resource '(.*)'")]
        public void WhenGETResourceWithAccept(string path)
        {
            var response = _dep.Browser.Get(path, context => context.Accept(new MediaRange(_mimeType)));
            ScenarioContext.Current.Set(response);
        }

        [Then(@"page (.*) of (.*) collection should have been retrieved")]
        public void ThenPageOfBookCollectionShouldHaveBeenRetrieved(int pageSize, string type)
        {
            A.CallTo(() => Impromptu.InvokeMember(_dep.Sources, "GetAll".WithGenericArgs(GetType(type)), pageSize, 10)).MustHaveHappened();
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

        [Then(@"(.*) '(.*)' should have been retrieved")]
        public void ThenBookShouldHaveBeenRetrieved(string type, string resourceUri)
        {
            Uri uri = new Uri(resourceUri);
            A.CallTo(() => Impromptu.InvokeMember(_dep.Sources, "Get".WithGenericArgs(GetType(type)), uri)).MustHaveHappened();
        }

        private Type GetType(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "book":
                    return typeof(Book);
                default:
                    throw new ArgumentException(string.Format("Unrecognized type {0}", typeName), typeName);
            }
        }
    }
}
