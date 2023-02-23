namespace BusinessLogic.Services
{
    public interface IService<TEntity, T1> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(T1 id);
        Task<TEntity> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(T1 id);
    }
}
