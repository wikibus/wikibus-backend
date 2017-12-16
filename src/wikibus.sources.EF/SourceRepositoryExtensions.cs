using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Wikibus.Sources.EF
{
    public static class SourceRepositoryExtensions
    {
        public static SearchableCollection<T> GetCollectionPage<T, TEntity>(
            this IDbSet<TEntity> dbSet,
            Uri identifier,
            Expression<Func<TEntity, object>> ordering,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> applyFilters,
            int page,
            int pageSize,
            Func<EntityWrapper<TEntity>, T> resourceFactory)
            where TEntity : class, IHasImage
        {
            var entireCollection = applyFilters(dbSet.AsNoTracking()).OrderBy(ordering);
            var pageOfBrochures = entireCollection.Skip((page - 1) * pageSize).Take(pageSize);
            var entityWrappers = pageOfBrochures.Select(entity => new EntityWrapper<TEntity>
            {
                Entity = entity,
                HasImage = entity.Image.Image != null
            });
            var books = entityWrappers.ToList();

            return new SearchableCollection<T>
            {
                Id = identifier,
                Members = books.ToList().Select(resourceFactory).ToArray(),
                TotalItems = entireCollection.Count()
            };
        }

        public static SearchableCollection<T> GetCollectionPage<T, TEntity>(
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

            return new SearchableCollection<T>
            {
                Id = identifier,
                Members = books.ToList().Select(resourceFactory).ToArray(),
                TotalItems = entireCollection.Count()
            };
        }
    }
}