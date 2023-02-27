using DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PersonRegistrationASPNet.BusinessLogic.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BusinessLogic.DTOs
{
    [Serializable, BsonIgnoreExtraElements]
    public class OrderDetailDto
    {
        [BsonElement("product_id"), BsonRepresentation(BsonType.Int32)]
        public int ProductId { get; set; }
        [BsonElement("quantity"), BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
        [BsonElement("unit_price"), BsonRepresentation(BsonType.Double)]
        public double UnitPrice { get; set; }

    public static implicit operator OrderDetail(OrderDetailDto orderDetail)
        {
            return new OrderDetail
            {
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice
            };
        }
    }
}
