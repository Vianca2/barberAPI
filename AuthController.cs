using Microsoft.AspNetCore.Mvc;
using BarberappAPI.DTOs;
using BarberappAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace BarberappAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroUsuarioDto registroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _authService.RegistrarUsuarioAsync(registroDto);

            if (resultado.Success)
            {
                return Ok(new { message = resultado.Message, usuario = resultado.Usuario });
            }

            return BadRequest(new { message = resultado.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _authService.LoginAsync(loginDto);

            if (resultado.Success)
            {
                return Ok(new
                {
                    message = resultado.Message,
                    usuario = resultado.Usuario,
                    token = resultado.Token
                });
            }

            return BadRequest(new { message = resultado.Message });
        }
    }
}