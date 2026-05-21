using Microsoft.EntityFrameworkCore;
using proyecto2Back.Modelos;

namespace proyecto2Back.Data
{
    public class ApplicationDbContext : DbContext
    {
        // El constructor configura la conexión (se llena automáticamente con lo que se agregue en appsettings.json)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Aquí registras tus modelos. DbSet significa "Crea una tabla en la BD basada en esta clase"
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
    }
}
