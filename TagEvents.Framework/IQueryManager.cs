namespace TegEvents.Framework
{
    public interface IQueryManager<TReadModel, TEntity> : IManager
        where TReadModel : class
        where TEntity : class, IEntity
    {
        Task<IEnumerable<TReadModel>> GetByIdAsync(Func<Task<List<TEntity>>> getlistOfEntities, long id, params long[] ids);

        Task<IEnumerable<TReadModel>> GetByIdAsync(Func<Task<List<TEntity>>> getlistOfEntities, IEnumerable<long> ids);

        Task<IEnumerable<TReadModel>> GetAllAsync(Func<Task<List<TEntity>>> getlistOfEntities);
    }
}
