using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class OrderDetail
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid integer number for Product ID! ")]
        [BsonElement("product_id"), BsonRepresentation(BsonType.Int32)]
        public int ProductId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid integer number for Quantity! ")]
        [BsonElement("quantity"), BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid double number for Unit Price. ")]
        [BsonElement("unit_price"), BsonRepresentation(BsonType.Double)]
        public double UnitPrice { get; set; }
    }
}
