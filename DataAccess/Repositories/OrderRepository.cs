using DataAccess.Configuration;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.Repositories
{
    public class OrderRepository : IRepository<Order, string>
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderRepository(IOptions<OrderProcessingOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var configuration = options.Value;
            var client = new MongoClient(configuration.DatabaseConnectionString);
            _database = client.GetDatabase(configuration.DatabaseName);
            _orderCollection = _database.GetCollection<Order>(configuration.CollectionName);
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
            await _orderCollection.InsertOneAsync(order);
            return order;
        }

        public async Task<Order> Update(Order order)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filterDefinition, order);
            return order;
        }

        public async Task Remove(string orderId)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
            await _orderCollection.DeleteOneAsync(filterDefinition);
        }

        public async Task Save() { }
    }
}
