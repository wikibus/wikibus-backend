using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using JsonLD.Entities;
using Nancy;
using Nancy.RDF;
using Nancy.RDF.Responses;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using NUnit.Framework;
using wikibus.common;
using wikibus.purl.nancy;

namespace wikibus.tests.Modules
{
    [TestFixture]
    public class RedirectionTests
    {
        private const string BaseUri = "http://wikibus.org/data/";
        private Browser _browser;

        [SetUp]
        public void Setup()
        {
            IResponseProcessor p = new ResponseProcessor();
            _browser = new Browser(c => c.Assembly("wikibus.purl.nancy")
                                         .Module<RedirectModule>()
                                         .DisableAutoRegistrations()
                                         .Dependency(A.Dummy<IEntitySerializer>())
                                         .Dependency<IWikibusConfiguration>(new TestConfiguration()));
        }

        [Test]
        public void Should_redirect_rdf_requests_to_document(
            [ValueSource("PathsToRedirect")] Tuple<string, string> path,
            [ValueSource("RdfMediaTypes")] RdfSerialization media)
        {
            // given
            var expectedReditectLocation = new Uri(BaseUri + string.Format("{0}", path.Item2));

            // when
            var response = _browser.Get(path.Item1, context => context.Accept(media.MediaType));

            // then
            response.StatusCode.Should().Be(HttpStatusCode.SeeOther);
            response.Headers["Location"].Should().Be(expectedReditectLocation.ToString());
        }

        private IEnumerable<Tuple<string, string>> PathsToRedirect()
        {
            yield return Tuple.Create("/brochure/", "brochure");
            yield return Tuple.Create("/brochure/x/y/z", "brochure/x/y/z");
            yield return Tuple.Create("brochure/x/y/z", "brochure/x/y/z");
        }

        private IEnumerable<RdfSerialization> RdfMediaTypes()
        {
            yield return RdfSerialization.Turtle;
            yield return RdfSerialization.RdfXml;
            yield return RdfSerialization.JsonLd;
            yield return RdfSerialization.NTriples;
            yield return RdfSerialization.Notation3;
        }
    }
}
