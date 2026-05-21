using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using proyecto2Back.Modelos;

namespace proyecto2Back.Modelos
{
    public enum Prioridad { Baja, Media, Alta }
    public enum Estado { Pendiente, EnProceso, Culminado }

    public class Tareas
    {
        [Key]
        public int IdTarea { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public int IdCreador { get; set; }
        [ForeignKey("IdCreador")]
        public Usuario Creador { get; set; } = null!;

        public int IdAsignado { get; set; }
        [ForeignKey("IdAsignado")]
        public Usuario Asignado { get; set; } = null!;

        public Prioridad Prioridad { get; set; }
        public Estado Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaLimite { get; set; }
        public DateTime? FechaCulminacion { get; set; }
        public bool? Activo { get; set; } = true;
    }
}