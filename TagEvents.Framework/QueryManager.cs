using AutoMapper;
using EnsureThat;
using Microsoft.Extensions.Logging;

namespace TegEvents.Framework
{
    public abstract class QueryManager<TEntity, TReadModel> : BaseQueryManagerExpression<TEntity, TReadModel>, IQueryManager<TReadModel, TEntity>
        where TEntity : class, IEntityWithId
        where TReadModel : class
    {
        protected QueryManager(ILogger<QueryManager<TEntity, TReadModel>> logger, IMapper mapper)
            : base(logger, mapper)
        {
        }

        public virtual async Task<IEnumerable<TReadModel>> GetByIdAsync(Func<Task<List<TEntity>>> getlistOfEntities, long id, params long[] ids)
        {
            EnsureArg.IsGt(id, 0, nameof(id));
            return await GetByIdAsync(getlistOfEntities, ids.Prepend(id));
        }

        public virtual async Task<IEnumerable<TReadModel>> GetByIdAsync(Func<Task<List<TEntity>>> getlistOfEntities, IEnumerable<long> ids)
        {
            EnsureArg.IsNotNull(ids, nameof(ids));
            EnsureArgExtensions.HasItems(ids, nameof(ids));

            return await GetByPredicateAsync(getlistOfEntities, x => ids.Contains(x.Id));
        }

        public virtual async Task<IEnumerable<TReadModel>> GetAllAsync(Func<Task<List<TEntity>>> getlistOfEntities)
        {
            return await GetByPredicateAsync(getlistOfEntities, x => true);
        }

        protected virtual async Task<IEnumerable<TReadModel>> GetByPredicateAsync(Func<Task<List<TEntity>>> getlistOfEntities, Func<TEntity, bool> predicate)
        {
            var result = await GetByQueryExpressionAsync(getlistOfEntities, predicate);
           
            return result.Models;
        }
    }
}
