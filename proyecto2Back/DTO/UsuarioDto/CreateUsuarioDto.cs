using proyecto2Back.Modelos;
using System.ComponentModel.DataAnnotations;

namespace proyecto2Back.DTO.UsuarioDto
{
    public class CreateUsuarioDto
    {
        [Required(ErrorMessage = "Debe ingresar primer nombre.")]
        public string? PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar primer apellido.")]
        public string? PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar username.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "El username es obligatorio.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar password.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "El password es obligatorio.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public Rol rol { get; set; }

        public bool? Activo { get; set; } = true;

    }
}
