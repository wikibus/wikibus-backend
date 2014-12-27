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
            Get["magazine/{magName}"] = GetResource<Magazine>;
            Get["books"] = GetPagedCollection<Book>;
            Get["brochures"] = GetPagedCollection<Brochure>;
            Get["magazines"] = GetPagedCollection<Magazine>;
        }

        private dynamic GetResource<T>(dynamic route) where T : class
        {
            return _repository.Get<T>(new Uri("http://wikibus.org" + Request.Path));
        }

        private dynamic GetPagedCollection<T>(dynamic route) where T : class
        {
            int page;
            if (!int.TryParse(Request.Query.page.Value, out page))
            {
                page = 1;
            }

            if (page < 0)
            {
                return 400;
            }

            return _repository.GetAll<T>(page);
        }
    }
}
