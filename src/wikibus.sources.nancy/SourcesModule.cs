using System;
using System.Collections.Generic;
using Hydra.Resources;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing.UriTemplates;
using TunnelVisionLabs.Net;
using wikibus.common;
using wikibus.sources.Filters;
using Id = wikibus.sources.IdentifierTemplates;

namespace wikibus.sources.nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public sealed class SourcesModule : UriTemplateModule
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

            Get(Id.BrochurePath, r => GetSingle(repository.GetBrochure));
            Get(Id.BookPath, r => GetSingle(repository.GetBook));
            Get(Id.MagazinePath, r => GetSingle(repository.GetMagazine));
            Get(Id.MagazineIssuesPath, r => GetSingle(repository.GetMagazineIssues) ?? new Collection<Issue>());
            Get(Id.MagazineIssuePath, r => GetSingle(repository.GetIssue));
            Get(Id.BrochuresPath, r => GetPage<Brochure, BrochureFilters>(Id.BrochuresPath, (int?)r.page, repository.GetBrochures));
            Get(Id.MagazinesPath, r => GetPage<Magazine, MagazineFilters>(Id.MagazinesPath, (int?)r.page, repository.GetMagazines));

            using (Templates)
            {
                Get(Id.BooksPath, r => GetPage<Book, BookFilters>(Id.BooksPath, (int?)r.page, repository.GetBooks, (object)r["params"].Value));
            }
        }

        private T GetSingle<T>(Func<Uri, T> getResource) where T : class
        {
            return getResource(GetRequestUri());
        }

        private Uri GetRequestUri()
        {
            return new Uri(new Uri(_config.BaseResourceNamespace), Request.Path);
        }

        private dynamic GetPage<T, TFilter>(string templatePath, int? page, Func<Uri, TFilter, int, int, Collection<T>> getPage, object parames = null)
            where T : class
        {
            if (page == null)
            {
                page = 1;
            }

            if (page < 0)
            {
                return 400;
            }

            var filter = this.Bind<TFilter>();
            var requestUri = GetRequestUri();
            var collection = getPage(requestUri, filter, page.Value, PageSize);

            var uriTemplate = new UriTemplate(_config.BaseResourceNamespace + templatePath);

            var templateParams = new Dictionary<string, object>
            {
                { "params", Context.Request.Query }
            };
            collection.Views = new IView[]
            {
                new TemplatedPartialCollectionView(uriTemplate, "page", collection.TotalItems, page.Value, PageSize, Context.Request.Query)
            };

            return collection;
        }
    }
}
