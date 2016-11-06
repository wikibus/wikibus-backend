using NullGuard;

namespace wikibus.sources.EF
{
    [NullGuard(ValidationFlags.None)]
    public class BrochureEntity : SourceEntity
    {
        public string FolderCode { get; set; }

        public string FolderName { get; set; }

        public string Notes { get; set; }
    }
}