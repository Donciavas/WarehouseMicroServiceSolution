using DataAccess.DTOs;
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
                return tEntity!;
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                return default!;
            }
        }
        public async Task<ResponseDto> Add(TEntity tEntity)
        {
            try
            {
                await _entities.AddAsync(tEntity);
                await _context.SaveChangesAsync();
                return new ResponseDto(true, "Entity was added");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return new ResponseDto(false, "Failed to add entity in the database");
            }
        }
        public async Task<ResponseDto> Update(TEntity tEntity)
        {
            try
            {
                _entities.Update(tEntity);
                await _context.SaveChangesAsync();
                return new ResponseDto(true, "Entity was updated");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return new ResponseDto(false, "Failed to update entity in the database");
            }
        }
        public async Task<ResponseDto> Remove(int id)
        {
            try
            {
                var entity = await _entities.FindAsync(id);
                if (entity is not null)
                {
                    _entities.Remove(entity);
                    await _context.SaveChangesAsync();
                    return new ResponseDto(true, "Entity was deleted");
                }
                return new ResponseDto(false, "Bad entity Id");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return new ResponseDto(false, "Failed to remove entity from the database");
            }
        }
    }
}
