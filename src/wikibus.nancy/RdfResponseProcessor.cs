using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Responses.Negotiation;

namespace wikibus.nancy
{
    public class RdfResponseProcessor : IResponseProcessor
    {
        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            throw new NotImplementedException();
        }

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get
            {
                yield return Tuple.Create("ttl", new MediaRange(RdfSerialization.Turtle.MediaType));
                yield return Tuple.Create("rdf", new MediaRange(RdfSerialization.RdfXml.MediaType));
                yield return Tuple.Create("jsonld", new MediaRange(RdfSerialization.JsonLd.MediaType));
                yield return Tuple.Create("n3", new MediaRange(RdfSerialization.Notation3.MediaType));
                yield return Tuple.Create("nt", new MediaRange(RdfSerialization.NTriples.MediaType));
            }
        }
    }
}
