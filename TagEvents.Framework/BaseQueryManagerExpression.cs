using AutoMapper;
using EnsureThat;
using Microsoft.Extensions.Logging;

namespace TegEvents.Framework
{
    public abstract class BaseQueryManagerExpression<TEntity, TReadModel>
        where TEntity : class, IEntity
        where TReadModel : class
    {
        protected BaseQueryManagerExpression(ILogger<BaseQueryManagerExpression<TEntity, TReadModel>> logger, IMapper mapper)
        {
            EnsureArg.IsNotNull(mapper, nameof(mapper));
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        protected virtual async Task<List<TEntity>> EntityQueryAsync(Func<Task<List<TEntity>>> getListOfEntities, Func<TEntity, bool> predicate)
        {
            EnsureArg.IsNotNull(predicate, nameof(predicate));
            EnsureArg.IsNotNull(getListOfEntities, nameof(getListOfEntities));

            var entities = await getListOfEntities();
            return entities.Where(predicate).ToList();
        }

#pragma warning disable SA1009
        protected virtual async Task<(List<TReadModel> Models, List<TEntity> Entities)> GetByQueryExpressionAsync(Func<Task<List<TEntity>>> getListOfEntities, Func<TEntity, bool> predicate)
        {
#pragma warning restore SA1009

            EnsureArg.IsNotNull(predicate, nameof(predicate));
            var results = new List<TReadModel>();
            var entities = await EntityQueryAsync(getListOfEntities, predicate);
            var models = Mapper.Map(entities, results);
            return new ValueTuple<List<TReadModel>, List<TEntity>>(models, entities);
        }
    }
}