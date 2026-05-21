using Microsoft.AspNetCore.Mvc;
using proyecto2Back.DTO.UsuarioDto;
using proyecto2Back.Servicios;

namespace proyecto2Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET /Usuario/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuarios();
            return Ok(usuarios);
        }

        // GET /Usuario/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetEstadisticas()
        {
            var (totalUsuarios, totalAdmin, totalAgente) = await _usuarioService.GetEstadisticas();
            return Ok(new { totalUsuarios, totalAdmin, totalAgente });
        }

        // GET /Usuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioById(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // POST /Usuario/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _usuarioService.CreateUsuario(dto);
            if (!resultado) return BadRequest("No se pudo registrar el usuario.");
            return Ok();
        }

        // PUT /Usuario/actualizar/{id}
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] ActualizarUsuarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _usuarioService.UpdateUsuario(id, dto);
            if (!resultado) return NotFound("Usuario no encontrado.");
            return Ok();
        }

        // DELETE /Usuario/eliminar/{id}
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var resultado = await _usuarioService.DeleteUsuario(id);
            if (!resultado) return NotFound("Usuario no encontrado.");
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var resultado = await _usuarioService.Login(dto);
            if (resultado == null) return Unauthorized("Credenciales incorrectas.");
            return Ok(resultado);
        }

        // POST /Usuario/login
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] loginIngreso.LoginDto dto)
        //{
        //    var resultado = await _usuarioService.Login(dto);
        //    if (resultado == null) return Unauthorized("Credenciales incorrectas.");
        //    return Ok(resultado);
        //}
    }
}
