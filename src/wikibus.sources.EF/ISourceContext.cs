using System.Data.Entity;

namespace wikibus.sources.EF
{
    public interface ISourceContext
    {
        IDbSet<BookEntity> Books { get; }

        IDbSet<BrochureEntity> Brochures { get; }

        IDbSet<MagazineEntity> Magazines { get; }
    }
}