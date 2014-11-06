using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;

namespace wikibus.tests
{
    [TestFixture]
    public class RedirectionTests
    {
        private Browser _browser;

        [SetUp]
        public void Setup()
        {
            _browser = new Browser(c => c.Assembly("wikibus.purl.nancy"));
        }

        [Test]
        public void Should_redirect_rdf_requests_to_document(
            [ValueSource("PathsToRedirect")] string path,
            [ValueSource("RdfMediaTypes")] MediaRange media)
        {
            // when
            var response = _browser.Get(path, context => context.Accept(media));

            // then
            response.StatusCode.Should().Be(HttpStatusCode.SeeOther);
            response.Headers["Location"].Should().Be(new Uri(new Uri("http://wikibus.org/"), path).ToString());
        }

        private IEnumerable<string> PathsToRedirect()
        {
            yield return "/brochure/";
        }

        private IEnumerable<MediaRange> RdfMediaTypes()
        {
            yield return new MediaRange("text/turtle");
        }
    }
}