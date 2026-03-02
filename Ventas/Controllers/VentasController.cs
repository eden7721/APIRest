using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ventas.Models;
using Ventas.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Ventas.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly VentasService _service;
        public VentasController(VentasService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> Get()
        {
            var todas_las_ventas = await _service.GetAllVentas();
            return Ok(todas_las_ventas);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Venta venta)
        {
            await _service.PostVenta(venta);
            return Created();
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> PostMultiple([FromBody] IEnumerable<Venta> ventas)
        {
            await _service.PostVentas(ventas);
            return Created();
        }


        [HttpGet("mes/{tipo}/{month}/{state}")]
        public async Task<ActionResult<IEnumerable<VentaTipo>>> GetTopThreeMonth(bool tipo, int month, bool state)
        {
            if(tipo)
            {
                var tres = await _service.GetThreeProductsMonthForAmount(month, state);
                return Ok(tres);
            }
            else
            {
                var tres = await _service.GetThreeProductsMonthForValue(month, state);
                return Ok(tres);
            }
        }
        [HttpGet("trimestre/{tipo}/{quarter}/{state}")]
        public async Task<ActionResult<IEnumerable<VentaTipo>>> GetTopThreeQuarter(bool tipo, int quarter, bool state)
        {
            if(tipo)
            {
                var tres = await _service.GetThreeProductsQuarterForAmount(quarter, state);
                return Ok(tres);
            }
            else
            {
                var tres = await _service.GetThreeProductsQuarterForValue(quarter, state);
                return (Ok(tres));
            }
        }
        [HttpGet("global/{tipo}/{state}")]
        public async Task<ActionResult<IEnumerable<VentaTipo>>> GetTopThreeGlobal(bool tipo, bool state)
        {
            if (tipo)
            {
                var tres = await _service.GetThreeProductsGlobalForAmount(state);
                return (Ok(tres));
            }
            else
            {
                var tres = await _service.GetThreeProductsGlobalForValue(state);
                return (Ok(tres));
            }
        }


        [HttpGet("categoria/{periodo}/{tipo}/{tiempo}/{state}")]
        public async Task<ActionResult<IEnumerable<VentaCategoria>>> GetTopThreeNoGlobal(string periodo, bool tipo, int tiempo, bool state)
        {
            if(periodo == "mes")
            {
                var tres = tipo ? await _service.GetProductsCategoryMonthForAmount(tiempo, state) : await _service.GetProductsCategoryMonthForValue(tiempo, state);
                return (Ok(tres));
            }
            else if(periodo == "trimestre")
            {
                var tres = tipo ? await _service.GetProductsCategoryQuarterForAmount(tiempo, state) : await _service.GetProductsCategoryQuarterForValue(tiempo, state);
                return (Ok(tres));
            }
            else
            {
                var tres = tipo ? await _service.GetProductsCategoryGlobalForAmount(state) : await _service.GetProductsCategoryGlobalForValue(state);
                return (Ok(tres));
            }
        }

    }
}
