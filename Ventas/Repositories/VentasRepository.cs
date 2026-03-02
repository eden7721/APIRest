using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using Ventas.Models;

namespace Ventas.Repositories
{ 
    public class VentasRepository
    {
        private readonly static string _connString = "mongodb://192.168.1.85:27017/?directConnection=true&serverSelectionTime\r\noutMS=2000&appName=mongosh+2.7.0";

        public async Task<IEnumerable<Venta>> GetVentas()
        {
            var db = await MongoDb("db_ventas");
            var ventaColl = db.GetCollection<Venta>("venta");

            var ventas = ventaColl.Find(e => e.Cliente != "").ToList();

            return ventas;
        }
        public async Task PostVenta(Venta venta)
        {
            var db = await MongoDb("db_ventas");
            var ventaColl = db.GetCollection<Venta>("venta");
            await ventaColl.InsertOneAsync(venta);
        }

        public async Task PostVentas(IEnumerable<Venta> ventas)
        {
            var db = await MongoDb("db_ventas");
            var ventaColl = db.GetCollection<Venta>("venta");
            await ventaColl.InsertManyAsync(ventas);
        }
        //DBMongo
        public static async Task<IMongoDatabase> MongoDb(string db)
        {
            var con = new MongoClient(_connString);
            var database = con.GetDatabase(db);
            return database;
        }
    }
}
