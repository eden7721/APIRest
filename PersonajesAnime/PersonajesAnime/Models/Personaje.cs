using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace PersonajesAnime.Models
{
    public class Personaje
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("edad")]
        public int Edad { get; set; }
        [BsonElement("anime")]
        public string Anime { get; set; }
    }
}
