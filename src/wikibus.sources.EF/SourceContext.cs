using System.Data.Entity;

namespace Wikibus.Sources.EF
{
    public class SourceContext : DbContext, ISourceContext
    {
        public SourceContext(ISourcesDatabaseSettings settings)
            : this(settings.ConnectionString)
        {
        }

        public SourceContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<SourceContext>(null);
        }

        public IDbSet<BookEntity> Books { get; set; }

        public IDbSet<BrochureEntity> Brochures { get; set; }

        public IDbSet<MagazineEntity> Magazines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SourceEntity>()
                .Map<BookEntity>(configuration => configuration.Requires("SourceType").HasValue("book"))
                .Map<BrochureEntity>(configuration => configuration.Requires("SourceType").HasValue("folder"))
                .Map<MagazineIssueEntity>(configuration => configuration.Requires("SourceType").HasValue("magissue"));

            modelBuilder.Entity<MagazineEntity>()
                .HasMany(t => t.Issues).WithRequired(issue => issue.Magazine).HasForeignKey(issue => issue.MagIssueMagazine);
        }
    }
}