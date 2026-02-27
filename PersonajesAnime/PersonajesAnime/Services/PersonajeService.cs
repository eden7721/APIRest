using PersonajesAnime.Models;
using PersonajesAnime.Respositories;

namespace PersonajesAnime.Services
{
    public class PersonajeService
    {
        private readonly PersonajesRepository repository;

        public PersonajeService(PersonajesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Personaje>> ObtenerTodosLosRegistros()
        {
            var obtenerTodosRegistros = await repository.GetPersonajes();
            return obtenerTodosRegistros;
        }
        public async Task CrearPersonaje(Personaje pj)
        {
            // No requerimos el id
            var pjWithoutId = new Personaje() { Nombre = pj.Nombre, Edad = pj.Edad, Anime = pj.Anime };

            await repository.PostPersonaje(pjWithoutId);
        }
    }
}
