using TareasApi.Application.DTOs;
using TareasApi.Application.Interfaces;
using TareasApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TareasApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {

        private readonly ITareaService _tareaService;
        private readonly ILoggerService _logger;

        public TareasController(ITareaService tareaService , ILoggerService logger) {
            _tareaService = tareaService;
            _logger = logger;
        }

        [HttpGet("{tareaId}")]
        public async Task<IActionResult> GetTarea(int tareaId)
        {
            var tarea = await _tareaService.GetTarea(tareaId);
            if (tarea == null)
            {   
                _logger.Log($"Intento fallido: Tarea no encontrada.{tareaId} ");
                return BadRequest(new ErrorDto { Codigo = 404, Mensaje = "Tarea no encontrada." });
            }

            return Ok(tarea);
        }

        [HttpGet]
        public async Task<IActionResult> GetTareas  ([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var resultados = await _tareaService.GetTareasAsync(pageNumber, pageSize);
            return Ok(resultados);
        }

        [HttpPost]
        public async Task<IActionResult> CreaTarea([FromBody] TareaDto tareaDto)
        {
            var success = await _tareaService.AddTareaAsync(tareaDto);
            if (!success)
            {
                _logger.Log($"Intento fallido: Ingreso de Tarea");
                return BadRequest(new ErrorDto { Codigo = 400, Mensaje = "Error al crear la Tarea." });
            }

            _logger.Log($"Nueva tarea registrado {tareaDto.Id}: {tareaDto.Titulo}");
            return CreatedAtAction(nameof(CreaTarea), tareaDto);
        }

        [HttpPut] //{tareaId}
        public async Task<IActionResult> UpdateTarea([FromBody] TareaDto tareaDto) //int tareaId,
        {
            var success = await _tareaService.UpdateTareaAsync(tareaDto);

            if (!success)
            {
                _logger.Log($"Intento fallido: Tarea {tareaDto.Id} no encontrada.");
                return BadRequest(new ErrorDto { Codigo = 400, Mensaje = "La tarea no existe." });
            }

            _logger.Log($"Tarea actualizada {tareaDto.Id}: {tareaDto.Titulo}");
            return Ok(tareaDto);
        }
        
        [HttpDelete("{tareaId}")]
        public async Task<IActionResult> DeleteTarea(int tareaId)
        {
            var eliminada = await _tareaService.DeleteTareaAsync(tareaId);

            if (!eliminada)
            {
                _logger.Log($"EliminaTarea - Tarea no encontrada {tareaId}");
                return NotFound(new { mensaje = "Tarea no encontrada." });
            } 
                
            _logger.Log($"Tarea Eliminada {tareaId}");
            return Ok(new { mensaje = "Tarea Eliminada correctamente." });
        }


    }
}
