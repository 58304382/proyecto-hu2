using static proyecto2Back.Modelos.Usuario;
using proyecto2Back.Modelos;

namespace proyecto2Back.DTO.UsuarioDto
{
    public class ReadUsuarioDto
    {
        public int IdUsuario { get; set; }
        public string? PrimerNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? NombreCompleto { get; set; }
        public Rol Rol { get; set; }
        public bool Activo { get; set; }

    }
}
