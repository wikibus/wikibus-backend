using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Hydra.Resources;

namespace Wikibus.Sources.EF
{
    public static class SourceRepositoryExtensions
    {
        public static Collection<T> GetCollectionPage<T, TEntity>(
            this IDbSet<TEntity> dbSet,
            Uri identifier,
            Expression<Func<TEntity, object>> ordering,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> applyFilters,
            int page,
            int pageSize,
            Func<TEntity, T> resourceFactory)
            where TEntity : class
        {
            var entireCollection = applyFilters(dbSet.AsNoTracking()).OrderBy(ordering);
            var pageOfBrochures = entireCollection.Skip((page - 1) * pageSize).Take(pageSize);
            var books = pageOfBrochures.ToList();

            return new Collection<T>
            {
                Id = identifier,
                Members = books.ToList().Select(resourceFactory).ToArray(),
                TotalItems = entireCollection.Count()
            };
        }
    }
}