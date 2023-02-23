using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity, int> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;
        protected readonly ILogger<Repository<TEntity>>? _logger;
        public Repository(DbContext context, ILogger<Repository<TEntity>> logger)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
            _logger = logger;
        }
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
            try
            {
                await _entities.AddAsync(tEntity);
                await _context.SaveChangesAsync();
                return tEntity;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return null!;
            }
        }
        public async Task<bool> Update(TEntity tEntity)
        {
            try
            {
                _entities.Update(tEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return false;
            }
        }
        public async Task<bool> Remove(int id)
        {
            try
            {
                var entity = await _entities.FindAsync(id);
                if (entity is not null)
                {
                    _entities.Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
