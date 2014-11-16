using System.IO;
using FakeItEasy;
using Nancy.RDF.Responses;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using wikibus.sources;
using wikibus.tests.Helpers;

namespace wikibus.tests
{
    [TestFixture]
    public class JsonLdSerializerTests
    {
        private IContextProvider _contextProvider;
        private JsonLdSerializer _serializer;
        private Stream _stream;

        [SetUp]
        public void Setup()
        {
            _contextProvider = A.Fake<IContextProvider>();
            _serializer = new JsonLdSerializer(_contextProvider);
            _stream = new MemoryStream();
        }

        [Test]
        public void Should_serialize_simple_object_in_given_context()
        {
            // given
            JToken context = new JValue("http://wikibus.org/context/brochure.jsonld");
            A.CallTo(() => _contextProvider.GetContext<Brochure>()).Returns(context);
            var model = new Brochure
            {
                Title = "Jelcz 123"
            };

            // when
            _serializer.Serialize(RdfSerialization.JsonLd.MediaType, model, _stream);
            var jsonLd = _stream.ToJsonObject();

            // then
            Assert.That(jsonLd.title.ToString(), Is.EqualTo("Jelcz 123"));
            Assert.That(jsonLd["@context"].ToString(), Is.EqualTo("http://wikibus.org/context/brochure.jsonld"));
        }
    }
}
