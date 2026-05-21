using proyecto2Back.Modelos;
using System.ComponentModel.DataAnnotations;

namespace proyecto2Back.DTO.UsuarioDto
{
    public class ActualizarUsuarioDto
    {

        [Required(ErrorMessage = "Debe ingresar primer nombre.")]
        public string PrimerNombre { get; set; } = string.Empty;

        public string? SegundoNombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar primer apellido.")]
        public string PrimerApellido { get; set; } = string.Empty;

        public string? SegundoApellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar username.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "El username debe tener mínimo 8 caracteres.")]
        public string Username { get; set; } = string.Empty;

        // Opcional, solo se actualiza si viene con valor
        [StringLength(100, MinimumLength = 8, ErrorMessage = "El password debe tener mínimo 8 caracteres.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public Rol rol { get; set; }
    }
}
