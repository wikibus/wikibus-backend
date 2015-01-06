using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Hydra;
using Nancy;
using Nancy.RDF;
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
        private readonly IList<KeyValuePair<string, string>> _queryString = new List<KeyValuePair<string, string>>();
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
            A.CallTo(() => _dep.Sources.GetBrochure(new Uri(resourceUri))).Returns(null);
        }

        [Given(@"existing brochure '(.*)'")]
        public void GivenExistingBrochure(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.GetBrochure(new Uri(resourceUri))).Returns(new Brochure());
        }

        [Given(@"existing magazine '(.*)'")]
        public void GivenExistingMagazine(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.GetMagazine(new Uri(resourceUri))).Returns(new Magazine());
        }

        [Given(@"exisiting book collection")]
        public void GivenExisitingBookCollection()
        {
            A.CallTo(() => _dep.Sources.GetBooks(A<Uri>.Ignored, A<int>.Ignored)).Returns(new PagedCollection<Book>());
        }

        [Given(@"existing book '(.*)'")]
        public void GivenExistingBook(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.GetBook(new Uri(resourceUri))).Returns(new Book());
        }

        [Given(@"exisiting image (.*)")]
        public void GivenExisitingImageForSource(int id)
        {
            A.CallTo(() => _dep.SourceImages.GetImageBytes(id)).Returns(new byte[] { 10 });
        }

        [Given(@"exisiting image for (.*) (.*)")]
        public void GivenExisitingImageForMag(string mag, string issue)
        {
            A.CallTo(() => _dep.SourceImages.GetImageBytes(mag, issue)).Returns(new byte[] { 10 });
        }

        [Given(@"query string is")]
        public void GivenQueryStringIs(Table queryString)
        {
            foreach (var query in queryString.Rows)
            {
                _queryString.Add(new KeyValuePair<string, string>(query.Values.ElementAt(0), query.Values.ElementAt(1)));
            }
        }

        [When(@"I GET resource '(.*)'")]
        public void WhenGETResourceWithAccept(string path)
        {
            var response = _dep.Browser.Get(path, SetupRequest);
            ScenarioContext.Current.Set(response);
        }

        [Then(@"content type should be '(.*)'")]
        public void ThenContentTypeShouldBe(string contentType)
        {
            ScenarioContext.Current.Get<BrowserResponse>().ContentType.Should().Be(contentType);
        }

        [Then(@"image (.*) should have been retrieved")]
        public void ThenImageShouldHaveBeenRetrieved(int id)
        {
            A.CallTo(() => _dep.SourceImages.GetImageBytes(id)).MustHaveHappened();
        }

        [Then(@"image (.*) (.*) should have been retrieved")]
        public void ThenIssueImageShouldHaveBeenRetrieved(string name, string issueNumber)
        {
            A.CallTo(() => _dep.SourceImages.GetImageBytes(name, issueNumber)).MustHaveHappened();
        }

        [Then(@"page (.*) of book collection should have been retrieved")]
        public void ThenPageOfBookCollectionShouldHaveBeenRetrieved(int page)
        {
            A.CallTo(() => _dep.Sources.GetBooks(A<Uri>.Ignored, page)).MustHaveHappened();
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
            A.CallTo(() => _dep.Sources.GetBrochure(new Uri(resourceUri))).MustHaveHappened();
        }

        [Then(@"book '(.*)' should have been retrieved")]
        public void ThenBookShouldHaveBeenRetrieved(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.GetBook(new Uri(resourceUri))).MustHaveHappened();
        }

        [Then(@"magazine '(.*)' should have been retrieved")]
        public void ThenMagazineShouldHaveBeenRetrieved(string resourceUri)
        {
            A.CallTo(() => _dep.Sources.GetMagazine(new Uri(resourceUri))).MustHaveHappened();
        }

        private void SetupRequest(BrowserContext context)
        {
            if (!string.IsNullOrWhiteSpace(_mimeType))
            {
                context.Accept(new MediaRange(_mimeType));
            }

            foreach (var query in _queryString)
            {
                context.Query(query.Key, query.Value);
            }
        }
    }
}
