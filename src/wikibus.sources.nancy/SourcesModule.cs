using System;
using Hydra;
using Nancy;
using wikibus.common;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public class SourcesModule : NancyModule
    {
        private readonly IWikibusConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesModule" /> class.
        /// </summary>
        /// <param name="repository">The source repository.</param>
        /// <param name="config">The configuration.</param>
        public SourcesModule(ISourcesRepository repository, IWikibusConfiguration config)
        {
            _config = config;

            this.ReturnNotFoundWhenModelIsNull();

            Get["brochure/{id}"] = r => GetSingle(repository.GetBrochure);
            Get["book/{id}"] = r => GetSingle(repository.GetBook);
            Get["magazine/{magName}"] = r => GetSingle(repository.GetMagazine);
            Get["magazine/{magName}/issues"] = r => GetSingle(repository.GetMagazineIssues) ?? new Collection<Issue>();
            Get["magazine/{magName}/issue/{number}"] = r => GetSingle(repository.GetIssue);
            Get["books"] = r => GetPage(repository.GetBooks);
            Get["brochures"] = r => GetPage(repository.GetBrochures);
            Get["magazines"] = r => GetPage(repository.GetMagazines);
        }

        private T GetSingle<T>(Func<Uri, T> getResource) where T : class
        {
            return getResource(GetRequestUri());
        }

        private Uri GetRequestUri()
        {
            return new Uri(new Uri(_config.BaseResourceNamespace), Request.Path);
        }

        private dynamic GetPage<T>(Func<Uri, int, PagedCollection<T>> getPage) where T : class
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

            return getPage(GetRequestUri(), page);
        }
    }
}
