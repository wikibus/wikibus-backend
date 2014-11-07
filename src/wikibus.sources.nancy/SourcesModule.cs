using System;
using Nancy;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public class SourcesModule : NancyModule
    {
        private readonly ISourcesRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesModule" /> class.
        /// </summary>
        /// <param name="repository">The source repository.</param>
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
