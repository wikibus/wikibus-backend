using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hydra.Annotations;
using NullGuard;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// A book about public transport
    /// </summary>
    public class Book : Source
    {
        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        [SupportedProperty(Schema.isbn)]
        public string ISBN { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        [SupportedProperty(Schema.author)]
        [AllowGet]
        public Author Author { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [SupportedProperty(DCTerms.title)]
        [Required]
        public string Title { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets the types.
        /// </summary>
        protected override IEnumerable<string> Types
        {
            get
            {
                foreach (var type in base.Types)
                {
                    yield return type;
                }

                yield return Wbo.Book;
            }
        }
    }
}
