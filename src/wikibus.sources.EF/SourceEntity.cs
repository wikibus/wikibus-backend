using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NullGuard;

namespace Wikibus.Sources.EF
{
    [NullGuard(ValidationFlags.None)]
    [Table("Source", Schema = "Sources")]
    public class SourceEntity : IHasImage
    {
        [Key]
        public int Id { get; set; }

        public string Language { get; set; }

        public string Language2 { get; set; }

        public int? Pages { get; set; }

        public short? Year { get; set; }

        public byte? Month { get; set; }

        public byte? Day { get; set; }

        public virtual ImageData Image { get; set; }
    }
}