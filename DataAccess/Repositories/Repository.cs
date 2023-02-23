using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity, int> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;
       public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
       public async Task<IEnumerable<TEntity>> GetAll()
       => await _entities.ToListAsync();
       public async Task<TEntity> Get(int id)
        => await _entities.FindAsync(id);
       public async Task<TEntity> Add(TEntity tEntity)
        {
            await _entities.AddAsync(tEntity);
            return tEntity;
        }
       public async Task<TEntity> Update(TEntity tEntity)
        {
            _entities.Update(tEntity);
            return tEntity;
        }
       public async Task Remove(int id)
        {
            var tEntity = await _entities.FindAsync(id);
            if (tEntity != null)
            {
                _entities.Remove(tEntity);
            }
        }
       public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
