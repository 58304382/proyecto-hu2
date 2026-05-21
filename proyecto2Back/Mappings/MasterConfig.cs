using Mapster;
using proyecto2Back.DTO.TareaDto;
using proyecto2Back.DTO.UsuarioDto;
using proyecto2Back.Modelos;

namespace proyecto2Back.Mappings
{
    public class MasterConfig
    {
        public static void RegisterMappings()
        {
            // 1. REGLAS PARA USUARIOS
            TypeAdapterConfig<Usuario, ReadUsuarioDto>.NewConfig()
                .Map(dto => dto.NombreCompleto,
                     src => $"{src.PrimerNombre} {src.SegundoNombre} {src.PrimerApellido} {src.SegundoApellido}")
                .Map(dest => dest.Rol,
                     src => src.Rol.ToString());

            // 2. REGLAS PARA TAREAS
            TypeAdapterConfig<Tareas, ReadTareaDto>.NewConfig()
                .Map(dest => dest.NombreCreador, src => src.Creador != null ? $"{src.Creador.PrimerNombre} {src.Creador.PrimerApellido}" : "Sin nombre")
                .Map(dest => dest.NombreAsignado, src => src.Asignado != null ? $"{src.Asignado.PrimerNombre} {src.Asignado.PrimerApellido}" : "No asignado")
                .Map(dest => dest.PrioridadNombre, src => src.Prioridad.ToString())
                .Map(dest => dest.EstadoNombre, src => src.Estado.ToString())
                .Map(dest => dest.IdCreador, src => src.IdCreador)
                .Map(dest => dest.IdAsignado, src => src.IdAsignado)
                .Map(dest => dest.Prioridad, src => (int)src.Prioridad)
                .Map(dest => dest.Estado, src => (int)src.Estado);
        }
    }
}