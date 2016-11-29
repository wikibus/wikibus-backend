using System;
using System.Linq;
using Argolis.Templates;
using Hydra.Resources;
using NullGuard;
using Wikibus.Common;
using Wikibus.Sources.Filters;

namespace Wikibus.Sources.EF
{
    public class SourcesRepository : ISourcesRepository
    {
        private readonly ISourceContext context;
        private readonly IdRetriever identifierRetriever;
        private readonly EntityFactory factory;
        private readonly IWikibusConfiguration config;

        public SourcesRepository(ISourceContext context, IdRetriever identifierRetriever, EntityFactory factory, IWikibusConfiguration config)
        {
            this.context = context;
            this.identifierRetriever = identifierRetriever;
            this.factory = factory;
            this.config = config;
        }

        [return: AllowNull]
        public Magazine GetMagazine(Uri identifier)
        {
            var id = this.identifierRetriever.GetMagazineName(identifier);

            if (id == null)
            {
                return null;
            }

            var source = (from mag in this.context.Magazines
                          where mag.Name == id
                          select mag).SingleOrDefault();

            if (source == null)
            {
                return null;
            }

            return this.factory.CreateMagazine(source);
        }

        [return: AllowNull]
        public Brochure GetBrochure(Uri identifier)
        {
            var id = IdentifierOf<Brochure>.Match(identifier, this.config.BaseResourceNamespace).Get<int?>("id");

            if (id == null)
            {
                return null;
            }

            var source = (from b in this.context.Brochures
                          where b.Id == id
                          select new EntityWrapper<BrochureEntity>
                          {
                              Entity = b,
                              HasImage = b.Image != null
                          }).SingleOrDefault();

            if (source == null)
            {
                return null;
            }

            return this.factory.CreateBrochure(source);
        }

        [return: AllowNull]
        public Book GetBook(Uri identifier)
        {
            var id = this.identifierRetriever.GetBookId(identifier);

            if (id == null)
            {
                return null;
            }

            var source = (from b in this.context.Books
                          where b.Id == id
                          select new EntityWrapper<BookEntity>
                          {
                              Entity = b,
                              HasImage = b.Image != null
                          }).SingleOrDefault();

            if (source == null)
            {
                return null;
            }

            return this.factory.CreateBook(source);
        }

        public Collection<Book> GetBooks(Uri identifier, BookFilters filters, int page, int pageSize = 10)
        {
            return this.context.Books.GetCollectionPage(
                identifier,
                entity => entity.BookTitle,
                this.FilterBooks(filters),
                page,
                pageSize,
                this.factory.CreateBook);
        }

        public Collection<Brochure> GetBrochures(Uri identifier, BrochureFilters filters, int page, int pageSize = 10)
        {
            return this.context.Brochures.GetCollectionPage(
                identifier,
                entity => entity.FolderName,
                this.FilterBrochures(filters),
                page,
                pageSize,
                this.factory.CreateBrochure);
        }

        public Collection<Magazine> GetMagazines(Uri identifier, MagazineFilters filters, int page, int pageSize = 10)
        {
            return this.context.Magazines.GetCollectionPage(
                identifier,
                entity => entity.Name,
                this.FilterMagazines(filters),
                page,
                pageSize,
                this.factory.CreateMagazine);
        }

        public Collection<Issue> GetMagazineIssues(Uri uri)
        {
            var name = this.identifierRetriever.GetMagazineForIssuesId(uri);

            if (name == null)
            {
                return new Collection<Issue>();
            }

            var results = (from m in this.context.Magazines
                           where m.Name == name
                           from issue in m.Issues
                           select new
                           {
                               Entity = issue,
                               issue.Magazine,
                               HasImage = issue.Image != null
                           }).ToList();

            var issues = results.Select(i => new EntityWrapper<MagazineIssueEntity>
            {
                Entity = i.Entity,
                HasImage = i.HasImage
            }).ToList();

            return new Collection<Issue>
            {
                Id = uri,
                Members = issues.Select(this.factory.CreateMagazineIssue).ToArray(),
                TotalItems = issues.Count
            };
        }

        [return: AllowNull]
        public Issue GetIssue(Uri identifier)
        {
            var id = this.identifierRetriever.GetIssueId(identifier);

            if (id == null)
            {
                return null;
            }

            var result = (from m in this.context.Magazines
                          where m.Name == id.MagazineName
                          from i in m.Issues
                          where i.MagIssueNumber == id.IssueNumber
                          select new
                          {
                              Entity = i,
                              i.Magazine,
                              HasImage = i.Image.Image != null
                          }).SingleOrDefault();

            if (result == null)
            {
                return null;
            }

            var source = new EntityWrapper<MagazineIssueEntity>
            {
                Entity = result.Entity,
                HasImage = result.HasImage
            };

            return this.factory.CreateMagazineIssue(source);
        }

        private Func<IQueryable<BookEntity>, IQueryable<BookEntity>> FilterBooks(BookFilters filters)
        {
            return books =>
            {
                if (string.IsNullOrWhiteSpace(filters.Title) == false)
                {
                    books = books.Where(e => e.BookTitle.Contains(filters.Title.Trim()));
                }

                if (string.IsNullOrWhiteSpace(filters.Author) == false)
                {
                    books = books.Where(e => e.BookAuthor.Contains(filters.Author.Trim()));
                }

                if (string.IsNullOrWhiteSpace(filters.Language) == false)
                {
                    books = books.Where(b => b.Language == filters.Language || b.Language2 == filters.Language);
                }

                return books;
            };
        }

        private Func<IQueryable<BrochureEntity>, IQueryable<BrochureEntity>> FilterBrochures(BrochureFilters filters)
        {
            return brochures =>
            {
                if (string.IsNullOrWhiteSpace(filters.Title) == false)
                {
                    brochures = brochures.Where(e => e.FolderName.Contains(filters.Title.Trim()));
                }

                if (string.IsNullOrWhiteSpace(filters.Language) == false)
                {
                    brochures = brochures.Where(b => b.Language == filters.Language || b.Language2 == filters.Language);
                }

                return brochures;
            };
        }

        private Func<IQueryable<MagazineEntity>, IQueryable<MagazineEntity>> FilterMagazines(MagazineFilters filters)
        {
            return entities =>
            {
                if (string.IsNullOrWhiteSpace(filters.Title) == false)
                {
                    entities = entities.Where(m => m.Name.Contains(filters.Title.Trim()));
                }

                return entities;
            };
        }
    }
}
