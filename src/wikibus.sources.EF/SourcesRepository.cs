﻿using System;
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
            return _context.Books.GetCollectionPage(
                identifier,
                entity => entity.BookTitle,
                FilterBooks(filters),
                page,
                pageSize,
                _factory.CreateBook);
        }

        public Collection<Brochure> GetBrochures(Uri identifier, BrochureFilters filters, int page, int pageSize = 10)
        {
            return _context.Brochures.GetCollectionPage(
                identifier,
                entity => entity.FolderName,
                FilterBrochures(filters),
                page,
                pageSize,
                _factory.CreateBrochure);
        }

        public Collection<Magazine> GetMagazines(Uri identifier, MagazineFilters filters, int page, int pageSize = 10)
        {
            return _context.Magazines.GetCollectionPage(
                identifier,
                entity => entity.Name,
                FilterMagazines(filters),
                page,
                pageSize,
                _factory.CreateMagazine);
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

        private Func<IQueryable<BookEntity>, IQueryable<BookEntity>> FilterBooks(BookFilters filters)
        {
            return entities =>
            {
                if (string.IsNullOrWhiteSpace(filters.Title) == false)
                {
                    entities = entities.Where(e => e.BookTitle.Contains(filters.Title.Trim()));
                }

                return entities;
            };
        }

        private Func<IQueryable<BrochureEntity>, IQueryable<BrochureEntity>> FilterBrochures(BrochureFilters filters)
        {
            return entities =>
            {
                if (string.IsNullOrWhiteSpace(filters.Title) == false)
                {
                    entities = entities.Where(e => e.FolderName.Contains(filters.Title.Trim()));
                }

                return entities;
            };
        }

        private Func<IQueryable<MagazineEntity>, IQueryable<MagazineEntity>> FilterMagazines(MagazineFilters filters)
        {
            return entities =>
            {
                return entities;
            };
        }
    }
}