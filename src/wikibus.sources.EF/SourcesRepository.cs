using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Argolis.Hydra.Resources;
using Argolis.Models;
using NullGuard;
using Wikibus.Sources.Filters;

namespace Wikibus.Sources.EF
{
    public class SourcesRepository : ISourcesRepository
    {
        private readonly ISourceContext context;
        private readonly EntityFactory factory;
        private readonly IUriTemplateMatcher matcher;

        public SourcesRepository(
            ISourceContext context,
            EntityFactory factory,
            IUriTemplateMatcher matcher)
        {
            this.context = context;
            this.factory = factory;
            this.matcher = matcher;
        }

        [return: AllowNull]
        public async Task<Magazine> GetMagazine(Uri identifier)
        {
            var id = this.matcher.Match<Magazine>(identifier).Get<string>("name");

            if (id == null)
            {
                return null;
            }

            var source = await (from mag in this.context.Magazines
                          where mag.Name == id
                          select mag).SingleOrDefaultAsync();

            if (source == null)
            {
                return null;
            }

            return this.factory.CreateMagazine(source);
        }

        [return: AllowNull]
        public async Task<Brochure> GetBrochure(Uri identifier)
        {
            var uriTemplateMatches = this.matcher.Match<Brochure>(identifier);
            var id = uriTemplateMatches.Get<int?>("id");

            if (id == null)
            {
                return null;
            }

            var source = await (from b in this.context.Brochures
                          where b.Id == id
                          select new EntityWrapper<BrochureEntity>
                          {
                              Entity = b,
                              HasImage = b.Image != null
                          }).SingleOrDefaultAsync();

            if (source == null)
            {
                return null;
            }

            return this.factory.CreateBrochure(source);
        }

        [return: AllowNull]
        public async Task<Book> GetBook(Uri identifier)
        {
            var id = this.matcher.Match<Book>(identifier).Get<int?>("id");

            if (id == null)
            {
                return null;
            }

            var source = await (from b in this.context.Books
                          where b.Id == id
                          select new EntityWrapper<BookEntity>
                          {
                              Entity = b,
                              HasImage = b.Image != null
                          }).SingleOrDefaultAsync();

            if (source == null)
            {
                return null;
            }

            return this.factory.CreateBook(source);
        }

        public async Task<SearchableCollection<Book>> GetBooks(Uri identifier, BookFilters filters, int page, int pageSize = 10)
        {
            return await this.context.Books.GetCollectionPage(
                identifier,
                entity => entity.BookTitle,
                this.FilterBooks(filters),
                page,
                pageSize,
                this.factory.CreateBook);
        }

        public async Task<SearchableCollection<Brochure>> GetBrochures(Uri identifier, BrochureFilters filters, int page, int pageSize = 10)
        {
            return await this.context.Brochures.GetCollectionPage(
                identifier,
                entity => entity.FolderName,
                this.FilterBrochures(filters),
                page,
                pageSize,
                this.factory.CreateBrochure);
        }

        public async Task<SearchableCollection<Magazine>> GetMagazines(Uri identifier, MagazineFilters filters, int page, int pageSize = 10)
        {
            return await this.context.Magazines.GetCollectionPage(
                identifier,
                entity => entity.Name,
                this.FilterMagazines(filters),
                page,
                pageSize,
                this.factory.CreateMagazine);
        }

        public async Task<Collection<Issue>> GetMagazineIssues(Uri uri)
        {
            var name = this.matcher.Match<Collection<Issue>>(uri).Get<string>("name");

            if (name == null)
            {
                return new Collection<Issue>();
            }

            var results = await (from m in this.context.Magazines
                           where m.Name == name
                           from issue in m.Issues
                           select new
                           {
                               Entity = issue,
                               issue.Magazine,
                               HasImage = issue.Image != null
                           }).ToListAsync();

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
        public async Task<Issue> GetIssue(Uri identifier)
        {
            var matches = this.matcher.Match<Issue>(identifier);

            if (matches.Success == false)
            {
                return null;
            }

            var magazineName = matches.Get<string>("name");
            var issueNumber = matches.Get<int>("number");

            var result = await (from m in this.context.Magazines
                          where m.Name == magazineName
                          from i in m.Issues
                          where i.MagIssueNumber == issueNumber
                          select new
                          {
                              Entity = i,
                              i.Magazine,
                              HasImage = i.Image.Image != null
                          }).SingleOrDefaultAsync();

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
