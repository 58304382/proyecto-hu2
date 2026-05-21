using proyecto2Back.DTO.UsuarioDto;

namespace proyecto2Back.Servicios
{
    public interface IUsuarioService
    {
        Task<bool> CreateUsuario(CreateUsuarioDto dto);
        Task<ReadUsuarioCompletoDto> GetUsuarioById(int id);
        Task<IEnumerable<ReadUsuarioDto>> GetAllUsuarios();
        Task<bool> DeleteUsuario(int id);
        Task<bool> UpdateUsuario(int id, ActualizarUsuarioDto dto);
        Task<(int totalUsuarios, int totalAdmin, int totalAgente)> GetEstadisticas();
        Task<LoginResponseDto?> Login(LoginRequestDto dto);
    }
}
