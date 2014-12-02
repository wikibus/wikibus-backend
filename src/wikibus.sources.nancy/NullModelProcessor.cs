using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Responses.Negotiation;

namespace wikibus.sources.nancy
{
    public class NullModelProcessor : IResponseProcessor
    {
        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            if (model == null)
            {
                return new ProcessorMatch { ModelResult = MatchResult.ExactMatch };
            }
            return new ProcessorMatch();
        }

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return 404;
        }

        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get { yield break; }
        }
    }
}