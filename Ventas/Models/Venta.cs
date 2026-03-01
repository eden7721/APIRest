using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ventas.Models
{
    public class Venta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("producto")]
        public string Producto { get; set; }
        [BsonElement("categoria")]
        public string Categoria { get; set; }
        [BsonElement("cliente")]
        public string Cliente { get; set; }
        [BsonElement("precio_unitario")]
        public decimal Precio_unitario { get; set; }
        [BsonElement("cantidad")]
        public int Cantidad { get; set; }
        [BsonElement("total_venta")]
        public decimal Total_venta { get; set; }
        [BsonElement("fecha_compra")]
        public DateTime Fecha_compra { get; set; }
    }
}
