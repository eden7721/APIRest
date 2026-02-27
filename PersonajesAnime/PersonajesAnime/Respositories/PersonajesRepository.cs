using MongoDB.Bson;
using MongoDB.Driver;
using PersonajesAnime.Models;
namespace PersonajesAnime.Respositories
{
    public class PersonajesRepository
    {
        private static string connectionString = "mongodb://localhost:27017";
        
        public async Task<List<Personaje>> GetPersonajes()
        {
            var mongoDB = await MongoDB("Personajes");
            var ColeccionPersonajes = mongoDB.GetCollection<Personaje>("personajes");
            var resultado = ColeccionPersonajes.Find(_ => true).ToList();
            return resultado;

        }
        public async Task PostPersonaje(Personaje pj)
        {
            var mongoDB = await MongoDB("Personajes");
            var ColeccionPersonajes = mongoDB.GetCollection<Personaje>("personajes");
            ColeccionPersonajes.InsertOne(pj);
        }

        public static async Task<IMongoDatabase> MongoDB(string Database)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(Database);
            return database;
        }
    }
}
