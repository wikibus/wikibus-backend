using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using VDS.RDF.Query.Builder;
using wikibus.nancy;
using wikibus.purl.nancy;
using wikibus.sources;
using wikibus.tests.FluentAssertions;

namespace wikibus.tests.Nancy
{
    [TestFixture]
    public class TurtleSerializerTests
    {
        private global::Nancy.ISerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new TurtleSerializer();
        }

        [Test]
        public void Should_serialize_source_to_turtle()
        {
            // given
            Stream output = new MemoryStream();
            const string title = "Jelcz 123";
            var model = new Brochure
                {
                    Title = title
                };

            // when
            _serializer.Serialize(RdfSerialization.Turtle.MediaType, model, output);

            // then
            output.Seek(0, SeekOrigin.Begin);
            output.AsRdf().Should().MatchAsk(
                  tpb => tpb.Subject("s").PredicateUri(new Uri("http://purl.org/dc/terms/title")).Object("title"),
                  exb => exb.Str(exb.Variable("title")) == title);
        }

        [Test]
        public void Should_acccept_turtle()
        {
            // given
            string mime = RdfSerialization.Turtle.MediaType;

            // when
            var canSerialize = _serializer.CanSerialize(mime);

            // then
            canSerialize.Should().BeTrue();
        }
    }
}
