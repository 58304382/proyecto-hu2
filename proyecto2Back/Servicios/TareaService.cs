using Mapster;
using Microsoft.EntityFrameworkCore;
using proyecto2Back.Data;
using proyecto2Back.DTO.TareaDto;
using proyecto2Back.Modelos;

namespace proyecto2Back.Servicios
{
    public class TareaService : ITareaService
    {
        private readonly ApplicationDbContext _conexion;

        public TareaService(ApplicationDbContext context)
        {
            _conexion = context;
        }

        public async Task<bool> CreateTarea(CreateTareaDto dto)
        {
            var tarea = new Tareas
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Prioridad = dto.Prioridad,
                Estado = dto.Estado,
                FechaCreacion = dto.FechaCreacion,
                FechaLimite = dto.FechaLimite,
                Activo = dto.Activo,

                IdCreador = dto.IdCreador,
                IdAsignado = dto.IdAsignado
            };

            _conexion.Tareas.Add(tarea);
            var filasAlteradas = await _conexion.SaveChangesAsync();

            return filasAlteradas > 0;
        }

        public async Task<CreateTareaDto> GetById(int idTarea)
        {
            // Buscamos por el ID de la tarea
            var tarea = await _conexion.Tareas
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.IdTarea == idTarea);

            if (tarea == null) return null;

            return tarea.Adapt<CreateTareaDto>();
        }

        public async Task<IEnumerable<ReadTareaDto>> GetAllTareas(int? idColaborador = null)
        {
            var query = _conexion.Tareas
                .AsNoTracking()  // trae los datos sin almacenarlos
                .Include(t => t.Creador)
                .Include(t => t.Asignado)
                .AsQueryable();

            if (idColaborador.HasValue)
            {
                query = query.Where(t => t.IdAsignado == idColaborador);
            }

            var tareas = await query.ToListAsync();

            return tareas.Adapt<IEnumerable<ReadTareaDto>>();
        }

        public async Task<bool> DeleteTarea(int id)
        {
            // Buscamos la tarea por su ID primario
            var tarea = await _conexion.Tareas
                .FirstOrDefaultAsync(t => t.IdTarea == id);

            // Retornar falso si no existe o si ya está inactiva (opcional)
            if (tarea == null || tarea.Activo == false)
            {
                return false;
            }

            // Aplicación de borrado lógico
            tarea.Activo = false;

            // Guardar los cambios en la base de datos
            var resultado = await _conexion.SaveChangesAsync();

            return resultado > 0;
        }


        public async Task<IEnumerable<ReadTareaDto>> GetTareasByFiltro(Prioridad? prioridad, Estado? estado, int? idUsuario)
        {
            // Empezamos con la consulta base
            var query = _conexion.Tareas.AsQueryable();

            // Si mandaron prioridad, agregamos el filtro a la cadena
            if (prioridad.HasValue)
            {
                query = query.Where(t => t.Prioridad == prioridad.Value);
            }

            // Si mandaron estado, agregamos el filtro
            if (estado.HasValue)
            {
                query = query.Where(t => t.Estado == estado.Value);
            }

            if (idUsuario.HasValue)
            {
                query = query.Where(t => t.IdAsignado == idUsuario.Value);
            }

            // Ejecutamos la consulta incluyendo las relaciones y el mapeo de Mapster
            return await query
                .AsNoTracking()  // trae los datos pero no los almacena
                .Include(t => t.Creador)  // Carga datos del creador para el DTO
                .Include(t => t.Asignado) // Carga datos del asignado
                .ProjectToType<ReadTareaDto>() // Usa las reglas que definimos en MasterConfig
                .ToListAsync();
        }


        public async Task<bool> UpdateTarea(int id, CreateTareaDto dto)
        {
            // Buscamos la tarea actual en la base de datos
            var tareaExistente = await _conexion.Tareas.FindAsync(id);

            if (tareaExistente == null)
            {
                return false;
            }

            // SOLO actualizamos los campos que interesan del DTO
            tareaExistente.Titulo = dto.Titulo!;
            tareaExistente.Descripcion = dto.Descripcion!;
            tareaExistente.IdCreador = dto.IdCreador;
            tareaExistente.IdAsignado = dto.IdAsignado;
            tareaExistente.Prioridad = dto.Prioridad;

            //  permitir editar la fecha límite solo si viene en el DTO
            if (dto.FechaLimite.HasValue)
            {
                tareaExistente.FechaLimite = dto.FechaLimite;
            }

            // Guardamos cambios
            // EF detecta que solo cambiaron algunos campos y genera el UPDATE solo para esos
            int filasAlteradas = await _conexion.SaveChangesAsync();

            return filasAlteradas > 0;
        }


        public async Task<bool> UpdateEstadoTarea(int id, int estado)
        {
            var tarea = await _conexion.Tareas.FindAsync(id);

            if (tarea == null) return false;

            tarea.Estado = (Estado)estado;

            if (estado == (int)Estado.Culminado)
                tarea.FechaCulminacion = DateTime.Now;

            await _conexion.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ReadTareaDto>> GetTareasByAgente(int idAgente)
        {
            var tareas = await _conexion.Tareas
                .Where(t => t.IdAsignado == idAgente && t.Activo == true)
                .Include(t => t.Creador)
                .Include(t => t.Asignado)
                .ToListAsync();

            return tareas.Adapt<IEnumerable<ReadTareaDto>>();
        }

        public async Task<(int totalTareas, int totalPendientes, int totalActivas, int totalCompletados)> GetEstadisticas(int? idColaborador = null)
        {
            // Creamos la consulta base, sin ejecutarla
            var query = _conexion.Tareas.AsQueryable();

            // Aplicamos el filtro solo si se proporciona un ID
            if (idColaborador.HasValue)
            {
                query = query.Where(t => t.IdAsignado == idColaborador.Value);
            }

            //Ejecutamos los conteos sobre la consulta (ya sea filtrada o general)
            int totalTareas = await query.CountAsync();
            int totalPendientes = await query.CountAsync(t => t.Estado == Estado.Pendiente);
            int totalActivas = await query.CountAsync(t => t.Estado == Estado.EnProceso);
            int totalCompletados = await query.CountAsync(t => t.Estado == Estado.Culminado);

            return (totalTareas, totalPendientes, totalActivas, totalCompletados);
        }

    }
}
