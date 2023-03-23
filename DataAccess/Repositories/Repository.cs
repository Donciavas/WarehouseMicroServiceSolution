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
        {
            try
            {
                var tEntities = await _entities.ToListAsync();
                _logger!.LogInformation("Returned all entities from database.");
                return tEntities;
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                return default!;
            }
        }
        public async Task<TEntity> Get(int id)
        {
            try
            {
                var tEntity = await _entities.FindAsync(id);
                _logger!.LogInformation("Returned entity by its unique ID.");
                return tEntity!;
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                return default!;
            }
        }
        public async Task<TEntity> Add(TEntity tEntity)
        {
            try
            {
                await _entities.AddAsync(tEntity);
                await _context.SaveChangesAsync();
                _logger!.LogInformation("Entity is added to database.");
                return tEntity;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return default!;
            }
        }
        public async Task<bool> Update(TEntity tEntity)
        {
            try
            {
                _entities.Update(tEntity);
                await _context.SaveChangesAsync();
                _logger!.LogInformation("Entity is updated and stored in database.");
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
                    _logger!.LogInformation("Entity is removed from database.");
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
