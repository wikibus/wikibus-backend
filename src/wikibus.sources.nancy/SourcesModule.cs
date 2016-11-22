﻿using System;
using System.Collections.Generic;
using Hydra.Resources;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing.UriTemplates;
using TunnelVisionLabs.Net;
using Wikibus.Common;
using Wikibus.Sources.Filters;
using Id = Wikibus.Sources.IdentifierTemplates;

namespace Wikibus.Sources.Nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public sealed class SourcesModule : UriTemplateModule
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

            this.Get(Id.BrochurePath, r => this.GetSingle(repository.GetBrochure));
            this.Get(Id.BookPath, r => this.GetSingle(repository.GetBook));
            this.Get(Id.MagazinePath, r => this.GetSingle(repository.GetMagazine));
            this.Get(Id.MagazineIssuesPath, r => this.GetSingle(repository.GetMagazineIssues) ?? new Collection<Issue>());
            this.Get(Id.MagazineIssuePath, r => this.GetSingle(repository.GetIssue));
            this.Get(Id.BrochuresPath, r => this.GetPage<Brochure, BrochureFilters>(Id.BrochuresPath, (int?)r.page, repository.GetBrochures));
            this.Get(Id.MagazinesPath, r => this.GetPage<Magazine, MagazineFilters>(Id.MagazinesPath, (int?)r.page, repository.GetMagazines));

            using (this.Templates)
            {
                this.Get(Id.BooksPath, r => this.GetPage<Book, BookFilters>(Id.BooksPath, (int?)r.page, repository.GetBooks));
            }
        }

        private T GetSingle<T>(Func<Uri, T> getResource)
            where T : class
        {
            return getResource(this.GetRequestUri());
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

            var filter = this.Bind<TFilter>();
            var requestUri = this.GetRequestUri();
            var collection = getPage(requestUri, filter, page.Value, PageSize);

            var uriTemplate = new UriTemplate(this.config.BaseResourceNamespace + templatePath);

            var templateParams = new Dictionary<string, object>((DynamicDictionary)this.Context.Request.Query);
            collection.Views = new IView[]
            {
                new TemplatedPartialCollectionView(uriTemplate, "page", collection.TotalItems, page.Value, PageSize, templateParams)
            };

            return collection;
        }
    }
}
