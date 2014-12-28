using System;
using Hydra;
using Nancy;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public class SourcesModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesModule" /> class.
        /// </summary>
        /// <param name="repository">The source repository.</param>
        public SourcesModule(ISourcesRepository repository)
        {
            this.ReturnNotFoundWhenModelIsNull();

            Get["brochure/{id}"] = r => GetSingle(repository.GetBrochure);
            Get["book/{id}"] = r => GetSingle(repository.GetBook);
            Get["magazine/{magName}"] = r => GetSingle(repository.GetMagazine);
            Get["books"] = r => GetPage(repository.GetBooks);
            Get["brochures"] = r => GetPage(repository.GetBrochures);
            Get["magazines"] = r => GetPage(repository.GetMagazines);
        }

        private T GetSingle<T>(Func<Uri, T> getResource) where T : class
        {
            return getResource(new Uri("http://wikibus.org" + Request.Path));
        }

        private dynamic GetPage<T>(Func<int, PagedCollection<T>> getPage) where T : class
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

            return getPage(page);
        }
    }
}
