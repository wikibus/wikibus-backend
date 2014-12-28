using System.ComponentModel.DataAnnotations;
using Hydra.Annotations;
using JsonLD.Entities;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// A book about public transport
    /// </summary>
    [Class("http://wikibus.org/ontology#Book")]
    public class Book : Source
    {
        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        [SupportedProperty("sch:isbn")]
        public string ISBN { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        [SupportedProperty("sch:author")]
        [AllowGet]
        public Author Author { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [SupportedProperty("dcterms:title")]
        [Required]
        public string Title { [return: AllowNull] get; set; }
    }
}
