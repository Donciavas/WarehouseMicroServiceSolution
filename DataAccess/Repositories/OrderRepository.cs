using DataAccess.Configuration;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    public class OrderRepository : IRepository<Order, string>
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Order> _orderCollection;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(IOptions<OrderProcessingOptions> options, ILogger<OrderRepository> logger)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var configuration = options.Value;
            var client = new MongoClient(configuration.DatabaseConnectionString);
            _database = client.GetDatabase(configuration.DatabaseName);
            _orderCollection = _database.GetCollection<Order>(configuration.CollectionName);
            _logger = logger;
        }
        public async Task<IEnumerable<Order>> GetAll()
         => await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        public async Task<Order> Get(string orderId)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
            return await _orderCollection.Find(filterDefinition).SingleOrDefaultAsync();
        }
        public async Task<Order> Add(Order order)
        {
            try
            {
                await _orderCollection.InsertOneAsync(order);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
            }
            return order;
        }
        public async Task<bool> Update(Order order)
        {
            try
            {
                var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
                if (filterDefinition is not null)
                {
                    await _orderCollection.ReplaceOneAsync(filterDefinition, order);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }
        public async Task<bool> Remove(string orderId)
        {
            try
            {
                var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
                if (filterDefinition is not null)
                {
                    await _orderCollection.DeleteOneAsync(filterDefinition);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
