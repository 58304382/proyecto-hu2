public class ReadTareaDto
{
    public int IdTarea { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string NombreCreador { get; set; } = string.Empty;
    public string NombreAsignado { get; set; } = string.Empty;
    public string PrioridadNombre { get; set; } = string.Empty;
    public string EstadoNombre { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaLimite { get; set; }
    public DateTime? FechaCulminacion { get; set; }
    public bool Activo { get; set; }
    public int IdCreador { get; set; }
    public int IdAsignado { get; set; }
    public int Prioridad { get; set; }
    public int Estado { get; set; }
}