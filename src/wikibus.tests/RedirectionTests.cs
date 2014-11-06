using System;
using System.Collections.Generic;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using wikibus.purl.nancy;

namespace wikibus.tests
{
    [TestFixture]
    public class RedirectionTests
    {
        private readonly Uri _baseUri = new Uri("http://wikibus.org/");
        private Browser _browser;

        [SetUp]
        public void Setup()
        {
            _browser = new Browser(c => c.Assembly("wikibus.purl.nancy"));
        }

        [Test]
        public void Should_redirect_rdf_requests_to_document(
            [ValueSource("PathsToRedirect")] string path,
            [ValueSource("RdfMediaTypes")] RdfMediaType media)
        {
            // given
            var expectedReditectLocation = new Uri(_baseUri, path + media.Extension);

            // when
            var response = _browser.Get(path, context => context.Accept(media.MediaType));

            // then
            response.StatusCode.Should().Be(HttpStatusCode.SeeOther);
            response.Headers["Location"].Should().Be(expectedReditectLocation.ToString());
        }

        private IEnumerable<string> PathsToRedirect()
        {
            yield return "/brochure/";
        }

        private IEnumerable<RdfMediaType> RdfMediaTypes()
        {
            yield return RdfMediaType.Turtle;
        }
    }
}
