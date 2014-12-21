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
        public SourcesModule(ISourcesRepository repository)
        {
            this.ReturnNotFoundWhenModelIsNull();

            _repository = repository;

            Get["brochure/{id}"] = GetResource<Brochure>;
            Get["book/{id}"] = GetResource<Book>;
            Get["books"] = GetPagedCollection<Book>;
            Get["brochures"] = GetPagedCollection<Brochure>;
        }

        private dynamic GetResource<T>(dynamic route) where T : Source
        {
            return _repository.Get<T>(new Uri("http://wikibus.org" + Request.Path));
        }

        private dynamic GetPagedCollection<T>(dynamic route) where T : Source
        {
            int page;

            if (!int.TryParse(Request.Query.page.Value, out page))
            {
                page = 1;
            }

            return _repository.GetAll<T>(page);
        }
    }
}
