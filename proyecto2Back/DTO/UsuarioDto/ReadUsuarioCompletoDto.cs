using proyecto2Back.Modelos;

namespace proyecto2Back.DTO.UsuarioDto
{
    public class ReadUsuarioCompletoDto
    {
        public int IdUsuario { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Username { get; set; }
        public Rol Rol { get; set; }
        public bool Activo { get; set; }
    }
}
