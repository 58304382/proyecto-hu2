using proyecto2Back.DTO.TareaDto;
using proyecto2Back.Modelos;


namespace proyecto2Back.Servicios
{
    public interface ITareaService
    {
        Task<bool> CreateTarea(CreateTareaDto dto);
        Task<CreateTareaDto> GetById(int idTarea);
        Task<IEnumerable<ReadTareaDto>> GetAllTareas(int? idColaborador = null);
        Task<bool> DeleteTarea(int id);
        Task<bool> UpdateTarea(int id, CreateTareaDto dto);
        Task<bool> UpdateEstadoTarea(int id, int estado);
        Task<IEnumerable<ReadTareaDto>> GetTareasByAgente(int idAgente);
        Task<IEnumerable<ReadTareaDto>> GetTareasByFiltro(Prioridad? prioridad, Estado? estado, int? idUsuario);
        Task<(int totalTareas, int totalPendientes, int totalActivas, int totalCompletados)> GetEstadisticas(int? idColaborador = null);
    }
}
