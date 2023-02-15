using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity, T1> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(T1 id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Remove(T1 id);
        Task Save();
        
    }
}
