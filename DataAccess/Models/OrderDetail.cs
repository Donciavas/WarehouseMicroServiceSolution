using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class OrderDetail
    {
        [BsonElement("product_id"), BsonRepresentation(BsonType.Int32)]
        public int ProductId { get; set; }
        [Range(1, 999999, ErrorMessage = "Values between 1 and 999999!")]
        [BsonElement("quantity"), BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
        [Range(1, 999999, ErrorMessage = "Values between 1 and 999999!")]
        [BsonElement("unit_price"), BsonRepresentation(BsonType.Double)]
        public double UnitPrice { get; set; }
    }
}
