using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NullGuard;

namespace Wikibus.Sources.EF
{
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
}