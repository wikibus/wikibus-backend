using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;
using wikibus.common.Vocabularies;

namespace wikibus.sources
{
    /// <summary>
    /// A book about public transport
    /// </summary>
    [SupportedClass(Wbo.Book)]
    public class Book : Source
    {
        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        public string ISBN { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        [Hydra.Annotations.Range(Schema.Person)]
        public Author Author { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
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
                yield return Schema.Book;
                yield return Bibo.Book;
            }
        }

        [UsedImplicitly]
        private static new JObject Context
        {
            get
            {
                var context = Source.Context;
                context.Add("isbn".IsProperty(Schema.isbn));
                context.Add("author".IsProperty(Schema.author));
                return context;
            }
        }
    }
}
