using System;
using System.Collections;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query.Builder;
using wikibus.nancy;
using wikibus.sources;
using wikibus.tests.FluentAssertions;

namespace wikibus.tests.Nancy
{
    [TestFixture]
    public class RdfSerializerTests
    {
        private global::Nancy.ISerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new RdfSerializer();
        }

        [Test]
        [TestCaseSource("RdfSerializations")]
        public void Should_serialize_source_to_rdf(RdfSerialization serialization, IRdfReader reader)
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
            output.AsGraph(reader).Should().MatchAsk(
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

        private IEnumerable RdfSerializations()
        {
            yield return new TestCaseData(RdfSerialization.Turtle, new TurtleParser());
            yield return new TestCaseData(RdfSerialization.NTriples, new NTriplesParser());
            yield return new TestCaseData(RdfSerialization.Notation3, new Notation3Parser());
            yield return new TestCaseData(RdfSerialization.RdfXml, new RdfXmlParser());
        }
    }
}
