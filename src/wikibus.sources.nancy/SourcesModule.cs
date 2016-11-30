using System;
using System.Collections.Generic;
using Argolis.Templates;
using Hydra.Resources;
using Nancy;
using Nancy.ModelBinding;
using TunnelVisionLabs.Net;
using Wikibus.Common;
using Wikibus.Sources.Filters;

namespace Wikibus.Sources.Nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public sealed class SourcesModule : ArgolisModule
    {
        private const int PageSize = 12;

        private readonly IWikibusConfiguration config;
        private readonly IModelTemplateProvider modelTemplateProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesModule" /> class.
        /// </summary>
        public SourcesModule(ISourcesRepository repository, IWikibusConfiguration config, IModelTemplateProvider modelTemplateProvider)
            : base(modelTemplateProvider)
        {
            this.config = config;
            this.modelTemplateProvider = modelTemplateProvider;

            this.ReturnNotFoundWhenModelIsNull();

            this.Get<Brochure>(r => this.GetSingle(repository.GetBrochure));
            this.Get<Book>(r => this.GetSingle(repository.GetBook));
            this.Get<Magazine>(r => this.GetSingle(repository.GetMagazine));
            this.Get<Collection<Issue>>(r => this.GetSingle(repository.GetMagazineIssues, new Collection<Issue>()));
            this.Get<Issue>(r => this.GetSingle(repository.GetIssue));

            using (this.Templates)
            {
                this.Get<Collection<Brochure>>(r => this.GetPage<Brochure, BrochureFilters>((int?)r.page, repository.GetBrochures));
                this.Get<Collection<Magazine>>(r => this.GetPage<Magazine, MagazineFilters>((int?)r.page, repository.GetMagazines));
                this.Get<Collection<Book>>(r => this.GetPage<Book, BookFilters>((int?)r.page, repository.GetBooks));
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

        private dynamic GetPage<T, TFilter>(int? page, Func<Uri, TFilter, int, int, Collection<T>> getPage)
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

            var uriTemplate = new UriTemplate(this.modelTemplateProvider.GetTemplate(typeof(Collection<T>)));
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
