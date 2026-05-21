namespace proyecto2Back.DTO.UsuarioDto
{
    public class LoginResponseDto
    {
        public int IdUsuario { get; set; }
        public string PrimerNombre { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
