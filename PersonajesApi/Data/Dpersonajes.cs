using Microsoft.Data.SqlClient;
using PersonajesApi.Dtos;
using System.Data;

namespace PersonajesApi.Data
{
    public class Dpersonajes
    {
        string connString = "data source=EDEN;initial catalog=Personajes;user=eden;password=12345;TrustServerCertificate=True";

        public async Task<List<PersonajeDto>> RecuperarInformacion()
        {
            var personajes = new List<PersonajeDto>();

            using var sql = new SqlConnection(connString);
            using var cmd = new SqlCommand("mostrarPersonajes", sql);

            cmd.CommandType = CommandType.StoredProcedure;
            await sql.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                personajes.Add(new PersonajeDto()
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1),
                    anime = reader.GetString(2)
                });
            }
            return personajes;
        }

        public async Task<PersonajeDto> RecuperarPorID(int id)
        {
            using var sql = new SqlConnection(connString);
            using var cmd = new SqlCommand("mostrarPersonaje", sql);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", id);

            await sql.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            var personaje = new PersonajeDto();
            if (await reader.ReadAsync())
            {
                personaje.id = reader.GetInt32(0);
                personaje.name = reader.GetString(1);
                personaje.anime = reader.GetString(2);
            }
            return personaje;
        }

        public async Task CrearPersonaje(PersonajeDto personaje)
        {
            using var sql = new SqlConnection(connString);
            using var cmd = new SqlCommand("insertarPersonaje", sql);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("nombre", personaje.name);
            cmd.Parameters.AddWithValue("anime", personaje.anime);

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ModificarPersonaje(int id, string name, string anime)
        {
            using var sql = new SqlConnection(connString);
            using var cmd = new SqlCommand("editarPersonajes", sql);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idPersonaje", id);
            cmd.Parameters.AddWithValue("nombre", name);
            cmd.Parameters.AddWithValue("anime", anime);

            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            
        }
        
        public async Task EliminarPersonaje(int id)
        {
            using var sql = new SqlConnection(connString);
            using var cmd = new SqlCommand("eliminarPersonajes", sql);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", id);
            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            
        }
    }
}
