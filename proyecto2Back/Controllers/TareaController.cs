using Microsoft.AspNetCore.Mvc;
using proyecto2Back.DTO.TareaDto;
using proyecto2Back.Modelos;
using proyecto2Back.Servicios;

namespace proyecto2Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TareaController : ControllerBase
    {

        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        // GET /Tarea/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTareas([FromQuery] int? idColaborador = null)
        {
            var tareas = await _tareaService.GetAllTareas(idColaborador);
            return Ok(tareas);
        }

        // GET /Tarea/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetEstadisticas([FromQuery] int? idColaborador = null)
        {
            var (totalTareas, totalPendientes, totalActivas, totalCompletados) = await _tareaService.GetEstadisticas(idColaborador);
            return Ok(new { totalTareas, totalPendientes, totalActivas, totalCompletados });
        }

        // GET /Tarea/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarea = await _tareaService.GetById(id);
            if (tarea == null) return NotFound();
            return Ok(tarea);
        }

        // GET /Tarea/agente/{idAgente}
        [HttpGet("agente/{idAgente}")]
        public async Task<IActionResult> GetTareasByAgente(int idAgente)
        {
            var tareas = await _tareaService.GetTareasByAgente(idAgente);
            return Ok(tareas);
        }

        // GET /Tarea/filtro
        [HttpGet("filtro")]
        public async Task<IActionResult> GetTareasByFiltro(
            [FromQuery] Prioridad? prioridad,
            [FromQuery] Estado? estado,
            [FromQuery] int? idUsuario)
        {
            var tareas = await _tareaService.GetTareasByFiltro(prioridad, estado, idUsuario);
            return Ok(tareas);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> CreateTarea([FromBody] CreateTareaDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(errores);
            }

            var resultado = await _tareaService.CreateTarea(dto);
            if (!resultado) return BadRequest("No se pudo crear la tarea.");
            return Ok();
        }

        // PUT /Tarea/actualizar/{id}
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> UpdateTarea(int id, [FromBody] CreateTareaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _tareaService.UpdateTarea(id, dto);
            if (!resultado) return NotFound("Tarea no encontrada.");
            return Ok();
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> UpdateEstadoTarea(int id, [FromBody] int estado)
        {
            var resultado = await _tareaService.UpdateEstadoTarea(id, estado);
            if (!resultado) return NotFound("Tarea no encontrada.");
            return Ok();
        }

        // DELETE /Tarea/eliminar/{id}
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var resultado = await _tareaService.DeleteTarea(id);
            if (!resultado) return NotFound("Tarea no encontrada.");
            return Ok();
        }
    }
}
