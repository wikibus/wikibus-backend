using System.IO;
using FluentAssertions;
using NUnit.Framework;
using wikibus.nancy;
using wikibus.purl.nancy;
using wikibus.sources;

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

            // when
            _serializer.Serialize(RdfSerialization.Turtle.MediaType, new Source(), output);

            // then
            output.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(output))
            {
                reader.ReadToEnd().Should().Be("12345");
            }
        }
    }
}
