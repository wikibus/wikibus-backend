﻿using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using JsonLD.Entities;
using Nancy;
using Nancy.Rdf;
using Nancy.Rdf.Contexts;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using NUnit.Framework;
using Wikibus.Common;
using Wikibus.Purl.Nancy;

namespace Wikibus.Tests.Modules
{
    [TestFixture]
    public class RedirectionTests
    {
        private const string BaseUri = "http://wikibus.org/data/";
        private Browser browser;

        [SetUp]
        public void Setup()
        {
            IResponseProcessor p = new ResponseProcessor();
            browser = new Browser(c => c
                                         .Module<RedirectModule>()
                                         .DisableAutoRegistrations()
                                         .Dependency(A.Dummy<IEntitySerializer>())
                                         .Dependency(A.Dummy<INamespaceManager>())
                                         .Dependency(A.Dummy<IContextPathMapper>())
                                         .Dependency<IWikibusConfiguration>(new TestConfiguration()));
        }

        [Test]
        public async void ShouldRedirectRDFRequestsToDocument(
            [ValueSource("PathsToRedirect")] Tuple<string, string> path,
            [ValueSource("RdfMediaTypes")] RdfSerialization media)
        {
            // given
            var expectedReditectLocation = new Uri(BaseUri + string.Format("{0}", path.Item2));

            // when
            var response = await browser.Get(path.Item1, context => context.Accept(media.MediaType));

            // then
            response.StatusCode.Should().Be(HttpStatusCode.SeeOther);
            response.Headers["Location"].Should().Be(expectedReditectLocation.ToString());
        }

        [Test]
        public async void ShouldRedirectRequestWithQuery()
        {
            // given
            const string pathExpected = "brochure/x/y/z?a=a&b=b+b";
            var expectedReditectLocation = new Uri(BaseUri + string.Format("{0}", pathExpected));

            // when
            var response = await browser.Get(
                "brochure/x/y/z",
                context =>
                {
                    context.Accept(RdfSerialization.Turtle.MediaType);
                    context.Query("a", "a");
                    context.Query("b", "b b");
                });

            // then
            response.StatusCode.Should().Be(HttpStatusCode.SeeOther);
            response.Headers["Location"].Should().Be(expectedReditectLocation.ToString());
        }

        private IEnumerable<Tuple<string, string>> PathsToRedirect()
        {
            yield return Tuple.Create("/brochure/", "brochure/");
            yield return Tuple.Create("/brochure/x/y/z", "brochure/x/y/z");
            yield return Tuple.Create("brochure/x/y/z", "brochure/x/y/z");
            yield return Tuple.Create("/", string.Empty);
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
