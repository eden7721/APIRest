using Microsoft.AspNetCore.Mvc;
using PersonajesApi.Data;
using PersonajesApi.Dtos;

namespace PersonajesApi.Controllers
{
    [ApiController]
    [Route("/personajes")]
    public class Endpoints : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<PersonajeDto>>> Get()
        {
            var funcion = new Dpersonajes();
            return await funcion.RecuperarInformacion();
        }

        [HttpGet("{id}")] //Get with argument
        public async Task<ActionResult<PersonajeDto>> GetForID(int id)
        {
            var funcion = new Dpersonajes();

            return await funcion.RecuperarPorID(id);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonajeDto pj)
        {
            var funcion = new Dpersonajes();
            await funcion.CrearPersonaje(pj);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Modificar(int id, [FromBody] PersonajeDto pj)
        {
            var funcion = new Dpersonajes();
            await funcion.ModificarPersonaje(id, pj.name, pj.anime);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Borrar(int id)
        {
            var funcion = new Dpersonajes();
            await funcion.EliminarPersonaje(id);

            return NoContent();
        }
    }
}
