using DataAccess.Configuration;
using DataAccess.DTOs;
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
        {
            try
            {
                var orders = await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                return default!;
            }
        }
        public async Task<Order> Get(string orderId)
        {
            try
            {
                var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
                return await _orderCollection.Find(filterDefinition).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex.Message, ex);
                return default!;
            }
        }
        public async Task<ResponseDto> Add(Order order)
        {
            try
            {
                await _orderCollection.InsertOneAsync(order);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return new ResponseDto(false, "Failed to add order in the database");
            }
            return new ResponseDto(true, "Order was added");
        }
        public async Task<ResponseDto> Update(Order order)
        {
            try
            {
                var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
                if (filterDefinition is not null)
                {
                    await _orderCollection.ReplaceOneAsync(filterDefinition, order);
                    return new ResponseDto(true, "Order was updated");
                }
                return new ResponseDto(false, "Bad order ID");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ResponseDto(false, "Failed to update order in the database");
            }
        }
        public async Task<ResponseDto> Remove(string orderId)
        {
            try
            {
                var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
                if (filterDefinition is not null)
                {
                    await _orderCollection.DeleteOneAsync(filterDefinition);
                    return new ResponseDto(true, "Order was deleted");
                }
                return new ResponseDto(false, "Bad order ID");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new ResponseDto(false, "Failed to remove order from the database");
            }
        }
    }
}
