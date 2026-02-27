using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonajesAnime.Models;
using PersonajesAnime.Respositories;
using PersonajesAnime.Services;

namespace PersonajesAnime.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly PersonajeService _personajeService;

        public PersonajesController(PersonajeService personajeService)
        {
            _personajeService = personajeService;
        }

        [HttpGet]
        public async Task<List<Personaje>> Get()
        {
            var obtenerPersonajes = await _personajeService.ObtenerTodosLosRegistros();
            return obtenerPersonajes;
        }

        [HttpPost]
        public async Task Post([FromBody] Personaje pj)
        {
            await _personajeService.CrearPersonaje(pj);
        }
    }
}
