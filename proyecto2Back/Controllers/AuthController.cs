using Microsoft.AspNetCore.Mvc;
using proyecto2Back.DTO.UsuarioDto;
using proyecto2Back.Servicios;

namespace proyecto2Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _authService;

        public AuthController(IUsuarioService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var resultado = await _authService.Login(dto);
            if (resultado == null) return Unauthorized("Credenciales incorrectas.");
            return Ok(resultado);
        }
    }
}