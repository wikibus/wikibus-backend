using NullGuard;

namespace Wikibus.Sources.EF
{
    [NullGuard(ValidationFlags.None)]
    public class MagazineIssueEntity : SourceEntity
    {
        public int MagIssueMagazine { get; set; }

        public MagazineEntity Magazine { get; set; }

        public int? MagIssueNumber { get; set; }
    }
}