using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BarberappAPI.DTOs;
using BarberappAPI.Services;
using System.Security.Claims;

namespace BarberappAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpPost]
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> CrearCita([FromBody] CrearCitaDto crearCitaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var resultado = await _citaService.CrearCitaAsync(clienteId, crearCitaDto);

            if (resultado.Success)
            {
                return Ok(new { message = resultado.Message, cita = resultado.Cita });
            }

            return BadRequest(new { message = resultado.Message });
        }

        [HttpGet("mis-citas")]
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> ObtenerMisCitas()
        {
            var clienteId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var resultado = await _citaService.ObtenerCitasClienteAsync(clienteId);

            if (resultado.Success)
            {
                return Ok(new { message = resultado.Message, citas = resultado.Citas });
            }

            return BadRequest(new { message = resultado.Message });
        }

        [HttpGet]
        [Authorize(Roles = "BARBERO")]
        public async Task<IActionResult> ObtenerTodasLasCitas()
        {
            var resultado = await _citaService.ObtenerTodasLasCitasAsync();

            if (resultado.Success)
            {
                return Ok(new { message = resultado.Message, citas = resultado.Citas });
            }

            return BadRequest(new { message = resultado.Message });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCitaPorId(long id)
        {
            var resultado = await _citaService.ObtenerCitaPorIdAsync(id);

            if (resultado.Success)
            {
                return Ok(new { message = resultado.Message, cita = resultado.Cita });
            }

            return NotFound(new { message = resultado.Message });
        }

        [HttpPut("{id}/estado")]
        [Authorize(Roles = "BARBERO")]
        public async Task<IActionResult> ActualizarEstadoCita(long id, [FromBody] ActualizarEstadoCitaDto actualizarDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _citaService.ActualizarEstadoCitaAsync(id, actualizarDto);

            if (resultado.Success)
            {
                return Ok(new { message = resultado.Message });
            }

            return BadRequest(new { message = resultado.Message });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "BARBERO")]
        public async Task<IActionResult> EliminarCita(long id)
        {
            var resultado = await _citaService.EliminarCitaAsync(id);

            if (resultado.Success)
            {
                return Ok(new { message = resultado.Message });
            }

            return BadRequest(new { message = resultado.Message });
        }
    }
}