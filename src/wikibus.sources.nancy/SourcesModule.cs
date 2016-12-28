using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Argolis.Hydra.Resources;
using Argolis.Models;
using Argolis.UriTemplates.Nancy;
using Nancy;
using Nancy.ModelBinding;
using TunnelVisionLabs.Net;
using Vocab;
using Wikibus.Sources.Filters;

namespace Wikibus.Sources.Nancy
{
    /// <summary>
    /// Module, which serves <see cref="Source"/>s
    /// </summary>
    public sealed class SourcesModule : ArgolisModule
    {
        private const int PageSize = 12;

        private readonly IModelTemplateProvider modelTemplateProvider;
        private readonly IUriTemplateExpander expander;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcesModule" /> class.
        /// </summary>
        public SourcesModule(
            ISourcesRepository repository,
            IModelTemplateProvider modelTemplateProvider,
            IUriTemplateExpander expander)
            : base(modelTemplateProvider)
        {
            this.modelTemplateProvider = modelTemplateProvider;
            this.expander = expander;

            this.ReturnNotFoundWhenModelIsNull();

            this.Get<Brochure>(async (r, c) => await this.GetSingle(repository.GetBrochure));
            this.Get<Book>(async (r, c) => await this.GetSingle(repository.GetBook));
            this.Get<Magazine>(async (r, c) => await this.GetSingle(repository.GetMagazine));
            this.Get<Collection<Issue>>(async (r, c) => await this.GetSingle(repository.GetMagazineIssues, new Collection<Issue>()));
            this.Get<Issue>(async (r, c) => await this.GetSingle(repository.GetIssue));

            using (this.Templates)
            {
                this.Get<Collection<Brochure>>(async (r, c) => await this.GetPage((int?)r.page, this.Bind<BrochureFilters>(), repository.GetBrochures));
                this.Get<Collection<Magazine>>(async (r, c) => await this.GetPage((int?)r.page, this.Bind<MagazineFilters>(), repository.GetMagazines));
                this.Get<Collection<Book>>(async (r, c) => await this.GetPage((int?)r.page, this.Bind<BookFilters>(), repository.GetBooks));
            }
        }

        private async Task<dynamic> GetSingle<T>(Func<Uri, Task<T>> getResource, T defaultValue = null)
            where T : class
        {
            var resource = await getResource(this.GetRequestUri()) ?? defaultValue;

            if (resource != null)
            {
                return this.Negotiate.WithModel(resource);
            }

            return new NotFoundResponse();
        }

        private Uri GetRequestUri()
        {
            return new Uri(this.Request.Path, UriKind.Relative);
        }

        private async Task<dynamic> GetPage<T, TFilter>(int? page, TFilter filter, Func<Uri, TFilter, int, int, Task<Collection<T>>> getPage)
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

            var uriTemplate = new UriTemplate(this.modelTemplateProvider.GetAbsoluteTemplate(typeof(Collection<T>)));
            var templateParams = new Dictionary<string, object>((DynamicDictionary)this.Context.Request.Query)
            {
                ["page"] = page
            };
            var contentLocation = this.expander.ExpandAbsolute<Collection<T>>(templateParams).ToString();

            var requestUri = this.GetRequestUri();
            var collection = await getPage(requestUri, filter, page.Value, PageSize);

            collection.Views = new IView[]
            {
                new TemplatedPartialCollectionView(uriTemplate, "page", collection.TotalItems, page.Value, PageSize, templateParams),
                new ViewTemplate(uriTemplate, this.GetFilterMappings<TFilter>())
            };

            return this.Negotiate.WithModel(collection).WithHeader("Content-Location", contentLocation);
        }

        private IEnumerable<IriTemplateMapping> GetFilterMappings<T>()
        {
            if (typeof(T) == typeof(BrochureFilters))
            {
                yield return new IriTemplateMapping("title", DCTerms.title);
                yield return new IriTemplateMapping("language", DCTerms.language);
            }
        }
    }
}
