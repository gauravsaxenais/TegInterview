namespace TegEvents.Framework.Blob
{
    public interface IBlobManager<TEntity> : IManager
        where TEntity : class, IEntity
    {
        Task<TEntity> GetListOfData();
    }
}
