using System;
using Hydra.Resources;
using Nancy;
using TunnelVisionLabs.Net;
using wikibus.common;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public class SourcesModule : NancyModule
    {
        private const int PageSize = 12;

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
            Get["books"] = r => GetPage<Book, Collection<Book>>(repository.GetBooks);
            Get["brochures"] = r => GetPage<Brochure, Collection<Brochure>>(repository.GetBrochures);
            Get["magazines"] = r => GetPage<Magazine, Collection<Magazine>>(repository.GetMagazines);
        }

        private T GetSingle<T>(Func<Uri, T> getResource) where T : class
        {
            return getResource(GetRequestUri());
        }

        private Uri GetRequestUri()
        {
            return new Uri(new Uri(_config.BaseResourceNamespace), Request.Path);
        }

        private dynamic GetPage<T, TCollection>(Func<Uri, int, int, TCollection> getPage)
            where T : class
            where TCollection : Collection<T>
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

            var collection = getPage(GetRequestUri(), page, PageSize);

            collection.Views = new[]
            {
                new TemplatedPartialCollectionView(new UriTemplate(Request.Url.Path + "{?page}"), "page", collection.TotalItems, page, PageSize)
            };

            return collection;
        }
    }
}
