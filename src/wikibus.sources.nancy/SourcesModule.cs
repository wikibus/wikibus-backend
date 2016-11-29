using System;
using System.Collections.Generic;
using Argolis.Templates;
using Hydra.Resources;
using Nancy;
using Nancy.ModelBinding;
using TunnelVisionLabs.Net;
using Wikibus.Common;
using Wikibus.Sources.Filters;
using Id = Wikibus.Sources.IdentifierTemplates;

namespace Wikibus.Sources.Nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public sealed class SourcesModule : ArgolisModule
    {
        private const int PageSize = 12;

        private readonly IWikibusConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesModule" /> class.
        /// </summary>
        /// <param name="repository">The source repository.</param>
        /// <param name="config">The configuration.</param>
        public SourcesModule(ISourcesRepository repository, IWikibusConfiguration config)
        {
            this.config = config;

            this.ReturnNotFoundWhenModelIsNull();

            this.Get<Brochure>(r => this.GetSingle(repository.GetBrochure));
            this.Get(Id.BookPath, r => this.GetSingle(repository.GetBook));
            this.Get(Id.MagazinePath, r => this.GetSingle(repository.GetMagazine));
            this.Get(Id.MagazineIssuesPath, r => this.GetSingle(repository.GetMagazineIssues, new Collection<Issue>()));
            this.Get(Id.MagazineIssuePath, r => this.GetSingle(repository.GetIssue));

            using (this.Templates)
            {
                this.Get(Id.BrochuresPath, r => this.GetPage<Brochure, BrochureFilters>(Id.BrochuresPath, (int?)r.page, repository.GetBrochures));
                this.Get(Id.MagazinesPath, r => this.GetPage<Magazine, MagazineFilters>(Id.MagazinesPath, (int?)r.page, repository.GetMagazines));
                this.Get(Id.BooksPath, r => this.GetPage<Book, BookFilters>(Id.BooksPath, (int?)r.page, repository.GetBooks));
            }
        }

        private dynamic GetSingle<T>(Func<Uri, T> getResource, T defaultValue = null)
            where T : class
        {
            var resource = getResource(this.GetRequestUri()) ?? defaultValue;

            if (resource != null)
            {
                return this.Negotiate.WithModel(resource);
            }

            return new NotFoundResponse();
        }

        private Uri GetRequestUri()
        {
            return new Uri(new Uri(this.config.BaseResourceNamespace), this.Request.Path);
        }

        private dynamic GetPage<T, TFilter>(string templatePath, int? page, Func<Uri, TFilter, int, int, Collection<T>> getPage)
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

            var uriTemplate = new UriTemplate(this.config.BaseResourceNamespace + templatePath);
            var templateParams = new Dictionary<string, object>((DynamicDictionary)this.Context.Request.Query)
            {
                ["page"] = page
            };
            var contentLocation = uriTemplate.BindByName(templateParams).ToString();

            var filter = this.Bind<TFilter>();
            var requestUri = this.GetRequestUri();
            var collection = getPage(requestUri, filter, page.Value, PageSize);

            collection.Views = new IView[]
            {
                new TemplatedPartialCollectionView(uriTemplate, "page", collection.TotalItems, page.Value, PageSize, templateParams)
            };

            return this.Negotiate.WithModel(collection).WithHeader("Content-Location", contentLocation);
        }
    }
}
