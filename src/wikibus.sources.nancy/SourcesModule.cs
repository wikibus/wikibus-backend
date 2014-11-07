using System;
using Nancy;

namespace wikibus.sources.nancy
{
    public class SourcesModule : NancyModule
    {
        private readonly ISourcesRepository _repository;

        public SourcesModule(ISourcesRepository repository) : base("data")
        {
            _repository = repository;

            Get["{path*}"] = GetResource;
        }

        private dynamic GetResource(dynamic route)
        {
            return _repository.Get<Source>(new Uri("http://wikibus.org/" + Request.Path.StripFirstSegment()));
        }
    }
}
