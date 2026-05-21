using proyecto2Back.Data;
using proyecto2Back.DTO.UsuarioDto;
using proyecto2Back.Modelos;
using Microsoft.EntityFrameworkCore;
using Mapster;
using BCrypt.Net;
using proyecto2Back.Modelos;

namespace proyecto2Back.Servicios
{
    public class UsuarioService : IUsuarioService
    {

        private readonly ApplicationDbContext _conexion;
        public UsuarioService(ApplicationDbContext context)
        {
            _conexion = context;
        }

        public async Task<bool> CreateUsuario(CreateUsuarioDto dto)
        {
            // se mapean los campo de ambas clases Usuario y createUsuarioDto, setea los datos de dto a usuario
            var usuario = dto.Adapt<Usuario>();
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);    // encriptar

            _conexion.Usuarios.Add(usuario);    // prepara para registrar
            var filasAlteradas = await _conexion.SaveChangesAsync();     // confirma registro

            return filasAlteradas > 0;
        }


        public async Task<ReadUsuarioCompletoDto> GetUsuarioById(int id)
        {
            var usuario = await _conexion.Usuarios
                .FirstOrDefaultAsync(usuario => usuario.IdUsuario == id);       // busca por id

            if (usuario == null) return null;

            // aplica configuración global de mapster
            return usuario.Adapt<ReadUsuarioCompletoDto>();
        }


        public async Task<IEnumerable<ReadUsuarioDto>> GetAllUsuarios()
        {
            var usuariosDb = await _conexion.Usuarios
                .AsNoTracking()
                .ToListAsync(); // trea la lista de la base de datos

            // indicamos que se realizará un mapeo y retornamos
            return usuariosDb.Adapt<IEnumerable<ReadUsuarioDto>>();
        }



        public async Task<bool> DeleteUsuario(int id)
        {
            var usuario = await _conexion.Usuarios.FirstOrDefaultAsync(usuario => usuario.IdUsuario == id);

            // retornar si no existe
            if (usuario == null)
            {
                return false;
            }

            // eliminación lógica
            usuario.Activo = !usuario.Activo;

            // guardar cambios
            var resultado = await _conexion.SaveChangesAsync();

            return resultado > 0;
        }

        public async Task<(int totalUsuarios, int totalAdmin, int totalAgente)> GetEstadisticas()
        {
            // Contamos todos sin importar el estado
            int totalUsuarios = await _conexion.Usuarios.CountAsync();

            // Contamos filtrando por el rol/tipo 
            int totalAdmin = await _conexion.Usuarios.CountAsync(u => u.Rol == Rol.Administrador);
            int totalAgente = await _conexion.Usuarios.CountAsync(u => u.Rol == Rol.Agente);

            return (totalUsuarios, totalAdmin, totalAgente);
        }

        public async Task<bool> UpdateUsuario(int id, ActualizarUsuarioDto dto)
        {
            // Buscar el usuario existente en la base de datos
            var usuarioExistente = await _conexion.Usuarios.FindAsync(id);

            if (usuarioExistente == null) return false;

            // Actualización de campos obligatorios y opcionales
            usuarioExistente.PrimerNombre = dto.PrimerNombre;
            usuarioExistente.SegundoNombre = dto.SegundoNombre;
            usuarioExistente.PrimerApellido = dto.PrimerApellido;
            usuarioExistente.SegundoApellido = dto.SegundoApellido;
            usuarioExistente.Username = dto.Username;
            usuarioExistente.Rol = dto.rol;

            // Lógica especial para el Password
            // Solo se actualiza si el usuario escribió algo nuevo en el campo
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                // aplicar hashing de contraseña antes de guardar
                usuarioExistente.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            //Guardar cambios
            return await _conexion.SaveChangesAsync() > 0;
        }

        public async Task<LoginResponseDto?> Login(LoginRequestDto dto)
        {
            var usuario = await _conexion.Usuarios
                .FirstOrDefaultAsync(u => u.Username == dto.Username && u.Activo);

            if (usuario == null) return null;

            bool contrasenaValida = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.Password);

            if (!contrasenaValida) return null;

            return new LoginResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                PrimerNombre = usuario.PrimerNombre,
                PrimerApellido = usuario.PrimerApellido,
                NombreCompleto = $"{usuario.PrimerNombre} {usuario.PrimerApellido}",
                Rol = ((int)usuario.Rol).ToString(),
                Activo = usuario.Activo,
                Token = "" // aquí irá el JWT si lo implementas después
            };


            //public async Task<loginRespuesta.LoginResponseDto?> Login(loginIngreso.LoginDto dto)
            //{
            //    var usuario = await _conexion.Usuarios
            //        .FirstOrDefaultAsync(u => u.Username == dto.username && u.Activo);

            //    if (usuario == null) return null;

            //    bool contrasenaValida = BCrypt.Net.BCrypt.Verify(dto.password, usuario.Password);

            //    if (!contrasenaValida) return null;

            //    return new loginRespuesta.LoginResponseDto
            //    {
            //        IdUsuario = usuario.IdUsuario,
            //        PrimerNombre = usuario.PrimerNombre,
            //        PrimerApellido = usuario.PrimerApellido,
            //        NombreCompleto = $"{usuario.PrimerNombre} {usuario.SegundoNombre} {usuario.PrimerApellido} {usuario.SegundoApellido}",
            //        Rol = ((int)usuario.Rol).ToString(),
            //        Activo = usuario.Activo,
            //        Token = ""
            //    };
            //}


        }
    }
}
