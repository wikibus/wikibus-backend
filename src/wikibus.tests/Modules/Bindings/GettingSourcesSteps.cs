using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Hydra.Resources;
using Nancy;
using Nancy.Rdf;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using TechTalk.SpecFlow;
using Wikibus.Sources;
using Wikibus.Sources.Filters;

namespace Wikibus.Tests.Modules.Bindings
{
    [Binding]
    public class NancyTestingSteps
    {
        private readonly NancyDependencies dep;
        private readonly IList<KeyValuePair<string, string>> queryString = new List<KeyValuePair<string, string>>();
        private string mimeType = RdfSerialization.Turtle.MediaType;

        public NancyTestingSteps(NancyDependencies dep)
        {
            this.dep = dep;
        }

        [Given(@"Accept header is '(.*)'")]
        public void GivenAcceptHeaderIs(string acceptHeader)
        {
            mimeType = acceptHeader;
        }

        [Given(@"brochure '(.*)' doesn't exist")]
        public void GivenBrochureDoesntExist(string resourceUri)
        {
            A.CallTo(() => dep.Sources.GetBrochure(new Uri(resourceUri))).Returns(Task.FromResult<Brochure>(null));
        }

        [Given(@"existing brochure '(.*)'")]
        public void GivenExistingBrochure(string resourceUri)
        {
            A.CallTo(() => dep.Sources.GetBrochure(new Uri(resourceUri))).Returns(new Brochure());
        }

        [Given(@"existing magazine '(.*)'")]
        public void GivenExistingMagazine(string resourceUri)
        {
            A.CallTo(() => dep.Sources.GetMagazine(new Uri(resourceUri))).Returns(new Magazine());
        }

        [Given(@"exisiting book collection")]
        public void GivenExisitingBookCollection()
        {
            A.CallTo(() => dep.Sources.GetBooks(A<Uri>.Ignored, A<BookFilters>.Ignored, A<int>.Ignored, A<int>.Ignored)).Returns(new Collection<Book>());
        }

        [Given(@"exisiting brochure collection")]
        public void GivenExisitingBrochureCollection()
        {
            A.CallTo(() => dep.Sources.GetBrochures(A<Uri>.Ignored, A<BrochureFilters>.Ignored, A<int>.Ignored, A<int>.Ignored)).Returns(new Collection<Brochure>());
        }

        [Given(@"exisiting magazine collection")]
        public void GivenExisitingMagazineCollection()
        {
            A.CallTo(() => dep.Sources.GetMagazines(A<Uri>.Ignored, A<MagazineFilters>.Ignored, A<int>.Ignored, A<int>.Ignored)).Returns(new Collection<Magazine>());
        }

        [Given(@"existing book '(.*)'")]
        public void GivenExistingBook(string resourceUri)
        {
            A.CallTo(() => dep.Sources.GetBook(new Uri(resourceUri))).Returns(new Book());
        }

        [Given(@"exisiting image (.*)")]
        public void GivenExisitingImageForSource(int id)
        {
            A.CallTo(() => dep.SourceImages.GetImageBytes(id)).Returns(new byte[] { 10 });
        }

        [Given(@"exisiting image for (.*) (.*)")]
        public void GivenExisitingImageForMag(string mag, string issue)
        {
            A.CallTo(() => dep.SourceImages.GetImageBytes(mag, issue)).Returns(new byte[] { 10 });
        }

        [Given(@"query string is")]
        public void GivenQueryStringIs(Table queryString)
        {
            foreach (var query in queryString.Rows)
            {
                this.queryString.Add(new KeyValuePair<string, string>(query.Values.ElementAt(0), query.Values.ElementAt(1)));
            }
        }

        [When(@"I GET resource '(.*)'")]
        public void WhenGetResourceWithAccept(string path)
        {
            var response = dep.Browser.Get(path, SetupRequest);
            ScenarioContext.Current.Set(response.Result);
        }

        [Then(@"content type should be '(.*)'")]
        public void ThenContentTypeShouldBe(string contentType)
        {
            ScenarioContext.Current.Get<BrowserResponse>().ContentType.Should().Be(contentType);
        }

        [Then(@"image (.*) should have been retrieved")]
        public void ThenImageShouldHaveBeenRetrieved(int id)
        {
            A.CallTo(() => dep.SourceImages.GetImageBytes(id)).MustHaveHappened();
        }

        [Then(@"image (.*) (.*) should have been retrieved")]
        public void ThenIssueImageShouldHaveBeenRetrieved(string name, string issueNumber)
        {
            A.CallTo(() => dep.SourceImages.GetImageBytes(name, issueNumber)).MustHaveHappened();
        }

        [Then(@"page (.*) of book collection should have been retrieved")]
        public void ThenPageOfBookCollectionShouldHaveBeenRetrieved(int page)
        {
            A.CallTo(() => dep.Sources.GetBooks(A<Uri>.Ignored, A<BookFilters>.Ignored, page, A<int>.Ignored)).MustHaveHappened();
        }

        [Then(@"page (.*) of magazine collection should have been retrieved")]
        public void ThenPageOfMagazineCollectionShouldHaveBeenRetrieved(int page)
        {
            A.CallTo(() => dep.Sources.GetMagazines(A<Uri>.Ignored, A<MagazineFilters>.Ignored, page, A<int>.Ignored)).MustHaveHappened();
        }

        [Then(@"page (.*) of brochure collection should have been retrieved")]
        public void ThenPageOfBrochureCollectionShouldHaveBeenRetrieved(int page)
        {
            A.CallTo(() => dep.Sources.GetBrochures(A<Uri>.Ignored, A<BrochureFilters>.Ignored, page, A<int>.Ignored)).MustHaveHappened();
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
            A.CallTo(() => dep.Sources.GetBrochure(new Uri(resourceUri))).MustHaveHappened();
        }

        [Then(@"book '(.*)' should have been retrieved")]
        public void ThenBookShouldHaveBeenRetrieved(string resourceUri)
        {
            A.CallTo(() => dep.Sources.GetBook(new Uri(resourceUri))).MustHaveHappened();
        }

        [Then(@"magazine '(.*)' should have been retrieved")]
        public void ThenMagazineShouldHaveBeenRetrieved(string resourceUri)
        {
            A.CallTo(() => dep.Sources.GetMagazine(new Uri(resourceUri))).MustHaveHappened();
        }

        private void SetupRequest(BrowserContext context)
        {
            if (!string.IsNullOrWhiteSpace(mimeType))
            {
                context.Accept(new MediaRange(mimeType));
            }

            foreach (var query in queryString)
            {
                context.Query(query.Key, query.Value);
            }
        }
    }
}
