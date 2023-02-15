using CustomerWebApi;
using CustomerWebApi.Models;
using MongoDB.Driver;
using OrderWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderRepository : IRepository<Order, string>
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderRepository()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _orderCollection = database.GetCollection<Order>("order");
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
