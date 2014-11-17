using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Responses.Negotiation;

namespace Nancy.RDF.Responses
{
    public class JsonLdResponseProcessor : IResponseProcessor
    {
        private ISerializer _serializer;

        public JsonLdResponseProcessor(IEnumerable<ISerializer> serializers)
        {
            _serializer = serializers.FirstOrDefault(s => s.CanSerialize(RdfSerialization.JsonLd.MediaType));
        }

        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return new ProcessorMatch
                {
                    ModelResult = MatchResult.DontCare,
                    RequestedContentTypeResult = MatchResult.ExactMatch
                };
        }

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return new Response
                {
                    Contents = stream => _serializer.Serialize(requestedMediaRange.ToString(), model, stream),
                    ContentType = RdfSerialization.JsonLd.MediaType,
                    StatusCode = HttpStatusCode.OK
                };
        }

        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get
            {
                yield return Tuple.Create(RdfSerialization.JsonLd.Extension, new MediaRange(RdfSerialization.JsonLd.MediaType));
            }
        }
    }
}