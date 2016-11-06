using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Hydra.Resources;
using NullGuard;
using wikibus.sources.Filters;

namespace wikibus.sources.EF
{
    public class SourcesRepository : ISourcesRepository
    {
        private readonly ISourceContext _context;
        private readonly IdRetriever _idRetriever;
        private readonly EntityFactory _factory;

        public SourcesRepository(ISourceContext context, IdRetriever idRetriever, EntityFactory factory)
        {
            _context = context;
            _idRetriever = idRetriever;
            _factory = factory;
        }

        [return: AllowNull]
        public Magazine GetMagazine(Uri identifier)
        {
            var id = _idRetriever.GetMagazineName(identifier);

            if (id == null)
            {
                return null;
            }

            var source = (from mag in _context.Magazines
                          where mag.Name == id
                          select mag).SingleOrDefault();

            if (source == null)
            {
                return null;
            }

            return _factory.CreateMagazine(source);
        }

        [return: AllowNull]
        public Brochure GetBrochure(Uri identifier)
        {
            var id = _idRetriever.GetBrochureId(identifier);

            if (id == null)
            {
                return null;
            }

            var source = (from b in _context.Brochures
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

            var brochure = _factory.CreateBrochure(source.Brochure);
            brochure.HasImage = source.HasImage;
            return brochure;
        }

        [return: AllowNull]
        public Book GetBook(Uri identifier)
        {
            var id = _idRetriever.GetBookId(identifier);

            if (id == null)
            {
                return null;
            }

            var source = (from b in _context.Books
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

            var brochure = _factory.CreateBook(source.Book);
            brochure.HasImage = source.HasImage;
            return brochure;
        }

        public Collection<Book> GetBooks(Uri identifier, BookFilters filters, int page, int pageSize = 10)
        {
            var query = (from entity in _context.Books
                         orderby entity.BookTitle
                         select entity).Skip((page - 1) * pageSize).Take(pageSize);
            var books = query.ToList();

            return new Collection<Book>
            {
                Id = identifier,
                Members = books.ToList().Select(_factory.CreateBook).ToArray(),
                TotalItems = query.Count()
            };
        }

        public Collection<Brochure> GetBrochures(Uri identifier, BrochureFilters filters, int page, int pageSize = 10)
        {
            var allBrochures = from entity in _context.Brochures
                               orderby entity.FolderName
                               select entity;
            var pageOfBrochures = allBrochures.Skip((page - 1) * pageSize).Take(pageSize);
            var books = pageOfBrochures.ToList();

            return new Collection<Brochure>
            {
                Id = identifier,
                Members = books.ToList().Select(_factory.CreateBrochure).ToArray(),
                TotalItems = allBrochures.Count()
            };
        }

        public Collection<Magazine> GetMagazines(Uri identifier, MagazineFilters filters, int page, int pageSize = 10)
        {
            var allBrochures = from entity in _context.Magazines
                               orderby entity.Name
                               select entity;
            var pageOfBrochures = allBrochures.Skip((page - 1) * pageSize).Take(pageSize);
            var books = pageOfBrochures.ToList();

            return new Collection<Magazine>
            {
                Id = identifier,
                Members = books.ToList().Select(_factory.CreateMagazine).ToArray(),
                TotalItems = allBrochures.Count()
            };
        }

        public Collection<Issue> GetMagazineIssues(Uri uri)
        {
            var name = _idRetriever.GetMagazineName(uri);

            if (name == null)
            {
                return new Collection<Issue>();
            }

            var issues = (from m in _context.Magazines
                          where m.Name == name
                          select m.Issues).SingleOrDefault();

            if (issues == null)
            {
                return new Collection<Issue>();
            }

            return new Collection<Issue>
            {
                Id = uri,
                Members = issues.Select(_factory.CreateMagazineIssue).ToArray(),
                TotalItems = issues.Count
            };
        }

        [return: AllowNull]
        public Issue GetIssue(Uri identifier)
        {
            var id = _idRetriever.GetIssueId(identifier);

            if (id == null)
            {
                return null;
            }

            var source = (from m in _context.Magazines
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

            var brochure = _factory.CreateMagazineIssue(source.Issue);
            brochure.HasImage = source.HasImage;
            return brochure;
        }
    }

    public class EntityFactory
    {
        private readonly IdentifierTemplates _templates;

        public EntityFactory(IdentifierTemplates templates)
        {
            _templates = templates;
        }

        public Book CreateBook(BookEntity bookEntity)
        {
            var book = new Book
            {
                Id = _templates.CreateBookIdentifier(bookEntity.Id)
            };
            if (bookEntity.BookISBN != null) book.ISBN = bookEntity.BookISBN;
            if (bookEntity.Pages != null) book.Pages = bookEntity.Pages;
            if (bookEntity.Year != null) book.Year = bookEntity.Year;
            if (bookEntity.Month != null) book.Month = bookEntity.Month;
            if (bookEntity.BookTitle != null) book.Title = bookEntity.BookTitle;
            if (bookEntity.BookAuthor != null)
            {
                book.Author = new Author
                {
                    Name = bookEntity.BookAuthor
                };
            }

            MapLanguages(book, bookEntity);
            MapDate(book, bookEntity);

            return book;
        }

        public Brochure CreateBrochure(BrochureEntity source)
        {
            var target = new Brochure
            {
                Id = _templates.CreateBrochureIdentifier(source.Id)
            };
            if (source.Notes != null) target.Description = source.Notes;
            if (source.FolderCode != null) target.Code = source.FolderCode;
            if (source.FolderName != null) target.Title = source.FolderName;

            MapSource(target, source);

            return target;
        }

        public Issue CreateMagazineIssue(MagazineIssueEntity issue)
        {
            var magazineIssue = new Issue
            {
                Id = _templates.CreateMagazineIssueIdentifier(issue.MagIssueNumber.Value, issue.Magazine.Name),
                Magazine = new Magazine
                {
                    Id = _templates.CreateMagazineIdentifier(issue.Magazine.Name)
                }
            };
            if (issue.MagIssueNumber != null) magazineIssue.Number = issue.MagIssueNumber.ToString();

            MapSource(magazineIssue, issue);

            return magazineIssue;
        }

        public Magazine CreateMagazine(MagazineEntity entity)
        {
            var magazine = new Magazine
            {
                Id = _templates.CreateMagazineIdentifier(entity.Name)
            };
            magazine.Title = entity.Name;
            return magazine;
        }

        private static void MapSource(Source target, SourceEntity source)
        {
            target.Month = source.Month;
            target.Year = source.Year;
            target.Pages = source.Pages;

            MapLanguages(target, source);
            MapDate(target, source);
        }

        private static void MapLanguages(Source target, SourceEntity source)
        {
            var languages = new List<string>();
            if (source.Language != null) languages.Add(source.Language);
            if (source.Language2 != null) languages.Add(source.Language2);
            target.Languages = languages.Select(l => new Language(l)).ToArray();
        }

        private static void MapDate(Source target, SourceEntity source)
        {
            if (source.Year.HasValue && source.Month.HasValue && source.Day.HasValue)
            {
                target.Date = new DateTime(source.Year.Value, source.Month.Value, source.Day.Value);
            }
        }
    }

    public interface ISourceContext
    {
        IDbSet<BookEntity> Books { get; }
        IDbSet<BrochureEntity> Brochures { get; }
        IDbSet<MagazineEntity> Magazines { get; }
    }

    public class SourceContext : DbContext, ISourceContext
    {
        public SourceContext(ISourcesDatabaseSettings settings) : this(settings.ConnectionString)
        {
        }

        public SourceContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SourceEntity>()
                .Map<BookEntity>(configuration => configuration.Requires("SourceType").HasValue("book"))
                .Map<BrochureEntity>(configuration => configuration.Requires("SourceType").HasValue("folder"))
                .Map<MagazineIssueEntity>(configuration => configuration.Requires("SourceType").HasValue("magissue"));

            modelBuilder.Entity<MagazineEntity>()
                .HasMany(t => t.Issues).WithRequired(issue => issue.Magazine).HasForeignKey(issue => issue.MagIssueMagazine);
        }

        public IDbSet<BookEntity> Books { get; set; }
        public IDbSet<BrochureEntity> Brochures { get; set; }
        public IDbSet<MagazineEntity> Magazines { get; set; }
    }

    [NullGuard(ValidationFlags.None)]
    [Table("Magazine", Schema = "Sources")]
    public class MagazineEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string SubName { get; set; }

        public IList<MagazineIssueEntity> Issues { get; set; }
    }

    [NullGuard(ValidationFlags.None)]
    public class BrochureEntity : SourceEntity
    {
        public string FolderCode { get; set; }
        public string FolderName { get; set; }
        public string Notes { get; set; }
    }

    [NullGuard(ValidationFlags.None)]
    [Table("Source", Schema = "Sources")]
    public class SourceEntity
    {
        [Key]
        public int Id { get; set; }

        public string Language { get; set; }
        public string Language2 { get; set; }
        public int? Pages { get; set; }
        public short? Year { get; set; }
        public byte? Month { get; set; }
        public byte? Day { get; set; }

        public byte[] Image { get; set; }
    }

    [NullGuard(ValidationFlags.None)]
    public class MagazineIssueEntity : SourceEntity
    {
        public int MagIssueMagazine { get; set; }

        public MagazineEntity Magazine { get; set; }

        public int? MagIssueNumber { get; set; }
    }

    [NullGuard(ValidationFlags.None)]
    public class BookEntity : SourceEntity
    {
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookISBN { get; set; }
    }
}
