using System;
using System.Collections.Generic;
using System.Linq;

namespace wikibus.sources.EF
{
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

            if (bookEntity.BookISBN != null)
            {
                book.ISBN = bookEntity.BookISBN.Trim();
            }

            if (bookEntity.Pages != null)
            {
                book.Pages = bookEntity.Pages;
            }

            if (bookEntity.Year != null)
            {
                book.Year = bookEntity.Year;
            }

            if (bookEntity.Month != null)
            {
                book.Month = bookEntity.Month;
            }

            if (bookEntity.BookTitle != null)
            {
                book.Title = bookEntity.BookTitle;
            }

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
            if (source.Notes != null)
            {
                target.Description = source.Notes;
            }

            if (source.FolderCode != null)
            {
                target.Code = source.FolderCode;
            }

            if (source.FolderName != null)
            {
                target.Title = source.FolderName;
            }

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
            if (issue.MagIssueNumber != null)
            {
                magazineIssue.Number = issue.MagIssueNumber.ToString();
            }

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
            if (source.Language != null)
            {
                languages.Add(source.Language);
            }

            if (source.Language2 != null)
            {
                languages.Add(source.Language2);
            }

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
}