using System;
using System.Collections.Generic;
using System.Linq;

namespace Wikibus.Sources.EF
{
    public class EntityFactory
    {
        private readonly IdentifierTemplates templates;

        public EntityFactory(IdentifierTemplates templates)
        {
            this.templates = templates;
        }

        public Book CreateBook(EntityWrapper<BookEntity> bookEntity)
        {
            var book = new Book
            {
                Id = this.templates.CreateBookIdentifier(bookEntity.Entity.Id)
            };

            if (bookEntity.Entity.BookISBN != null)
            {
                book.ISBN = bookEntity.Entity.BookISBN.Trim();
            }

            if (bookEntity.Entity.Pages != null)
            {
                book.Pages = bookEntity.Entity.Pages;
            }

            if (bookEntity.Entity.Year != null)
            {
                book.Year = bookEntity.Entity.Year;
            }

            if (bookEntity.Entity.Month != null)
            {
                book.Month = bookEntity.Entity.Month;
            }

            if (bookEntity.Entity.BookTitle != null)
            {
                book.Title = bookEntity.Entity.BookTitle;
            }

            if (bookEntity.Entity.BookAuthor != null)
            {
                book.Author = new Author
                {
                    Name = bookEntity.Entity.BookAuthor
                };
            }

            book.HasImage = bookEntity.HasImage;

            MapLanguages(book, bookEntity.Entity);
            MapDate(book, bookEntity.Entity);

            return book;
        }

        public Brochure CreateBrochure(EntityWrapper<BrochureEntity> source)
        {
            var target = new Brochure
            {
                Id = this.templates.CreateBrochureIdentifier(source.Entity.Id)
            };
            if (source.Entity.Notes != null)
            {
                target.Description = source.Entity.Notes;
            }

            if (source.Entity.FolderCode != null)
            {
                target.Code = source.Entity.FolderCode;
            }

            if (source.Entity.FolderName != null)
            {
                target.Title = source.Entity.FolderName;
            }

            target.HasImage = source.HasImage;

            MapSource(target, source.Entity);

            return target;
        }

        public Issue CreateMagazineIssue(EntityWrapper<MagazineIssueEntity> issue)
        {
            var magazineIssue = new Issue
            {
                Id = this.templates.CreateMagazineIssueIdentifier(issue.Entity.MagIssueNumber.Value, issue.Entity.Magazine.Name),
                Magazine = new Magazine
                {
                    Id = this.templates.CreateMagazineIdentifier(issue.Entity.Magazine.Name)
                }
            };
            if (issue.Entity.MagIssueNumber != null)
            {
                magazineIssue.Number = issue.Entity.MagIssueNumber.ToString();
            }

            magazineIssue.HasImage = issue.HasImage;

            MapSource(magazineIssue, issue.Entity);

            return magazineIssue;
        }

        public Magazine CreateMagazine(MagazineEntity entity)
        {
            var magazine = new Magazine
            {
                Id = this.templates.CreateMagazineIdentifier(entity.Name)
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