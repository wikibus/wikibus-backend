using System;
using System.Linq;
using Hydra.Resources;
using NullGuard;
using Wikibus.Sources.Filters;

namespace Wikibus.Sources.EF
{
    public class SourcesRepository : ISourcesRepository
    {
        private readonly ISourceContext context;
        private readonly IdRetriever identifierRetriever;
        private readonly EntityFactory factory;

        public SourcesRepository(ISourceContext context, IdRetriever identifierRetriever, EntityFactory factory)
        {
            this.context = context;
            this.identifierRetriever = identifierRetriever;
            this.factory = factory;
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
            var id = this.identifierRetriever.GetBrochureId(identifier);

            if (id == null)
            {
                return null;
            }

            var source = (from b in this.context.Brochures
                          where b.Id == id
                          select new
                          {
                              Brochure = b,
                              HasImage = b.Image != null
                          }).SingleOrDefault();

            if (source == null)
            {
                return null;
            }

            var brochure = this.factory.CreateBrochure(source.Brochure);
            brochure.HasImage = source.HasImage;
            return brochure;
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
                          select new
                          {
                              Book = b,
                              HasImage = b.Image != null
                          }).SingleOrDefault();

            if (source == null)
            {
                return null;
            }

            var brochure = this.factory.CreateBook(source.Book);
            brochure.HasImage = source.HasImage;
            return brochure;
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
            var name = this.identifierRetriever.GetMagazineName(uri);

            if (name == null)
            {
                return new Collection<Issue>();
            }

            var issues = (from m in this.context.Magazines
                          where m.Name == name
                          select m.Issues).SingleOrDefault();

            if (issues == null)
            {
                return new Collection<Issue>();
            }

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

            var source = (from m in this.context.Magazines
                          where m.Name == id.MagazineName
                          from i in m.Issues
                          where i.MagIssueNumber == id.IssueNumber
                          select new
                          {
                              Issue = i,
                              i.Magazine,
                              HasImage = i.Image != null
                          }).SingleOrDefault();

            if (source == null)
            {
                return null;
            }

            var brochure = this.factory.CreateMagazineIssue(source.Issue);
            brochure.HasImage = source.HasImage;
            return brochure;
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
