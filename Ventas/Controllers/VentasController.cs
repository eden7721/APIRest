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


        [HttpGet("mescantidad/{month}/{state}")]
        public async Task<ActionResult<IEnumerable<VentaCantidad>>> GetTopThreeMonthForAmount(int month, bool state)
        {
            var tres = await _service.GetThreeProductsMonthForAmount(month, state);
            return Ok(tres);
        }


        [HttpGet("trimestrecantidad/{quarter}/{state}")]
        public async Task<ActionResult<IEnumerable<VentaCantidad>>> GetTopThreeQuarterForAmount(int quarter, bool state)
        {
            var tres = await _service.GetThreeProductsQuarterForAmount(quarter, state);
            return Ok(tres);
        }


        [HttpGet("mesvalor/{month}/{state}")]
        public async Task<ActionResult<IEnumerable<VentaCantidad>>> GetTopThreeMonthForValue(int month, bool state)
        {
            var tres = await _service.GetThreeProductsMonthForValue(month, state);
            return (Ok(tres));
        }

        [HttpGet("trimestrevalor/{quarter}/{state}")]
        public async Task<ActionResult<IEnumerable<VentaMonto>>> GetTopThreeQuarterForValue(int quarter, bool state)
        {
            var tres = await _service.GetThreeProductsQuarterForValue(quarter, state);
            return (Ok(tres));
        }


        [HttpGet("globalcantidad/{state}")]
        public async Task<ActionResult<IEnumerable<VentaCantidad>>> GetTopThreeGlobalForAmount(bool state)
        {
            var tres = await _service.GetThreeProducstGlobalForAmount(state);
            return (Ok(tres));
        }


        [HttpGet("globalvalor/{state}")]
        public async Task<ActionResult<IEnumerable<VentaCantidad>>> GetTopThreeGlobalForValue(bool state)
        {
            var tres = await _service.GetThreeProducstGlobalForValue(state);
            return (Ok(tres));
        }
    }
}
