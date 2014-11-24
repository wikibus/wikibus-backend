using System;
using FakeItEasy;
using Nancy.RDF.Responses;
using Nancy.Testing;
using NUnit.Framework;
using wikibus.sources;
using wikibus.sources.nancy;

namespace wikibus.tests.Modules
{
    [TestFixture]
    public class SourcesModuleTests
    {
        private ISourcesRepository _sources;
        private Browser _browser;

        [SetUp]
        public void Setup()
        {
            _sources = A.Fake<ISourcesRepository>();
            _browser = new Browser(with => with.Module<SourcesModule>().Dependency(_sources));
        }

        [Test]
        public void Should_query_for_correct_resource()
        {
            // given
            const string path = "/data/brochure/12345";
            Uri expectedIdentifier = new Uri("http://wikibus.org/brochure/12345");

            // when
            _browser.Get(path, with => with.Accept(RdfSerialization.Turtle.MediaType));

            // then
            A.CallTo(() => _sources.Get<Source>(expectedIdentifier)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
