using Microsoft.EntityFrameworkCore;
using proyecto2Back.Data;
using proyecto2Back.Modelos;
using proyecto2Back.Servicios;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("No existe la cadena de conexión 'DefaultConnection'.");
}

// Versión MySQL
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

// MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITareaService, TareaService>();

// Mapster
proyecto2Back.Mappings.MasterConfig.RegisterMappings();

var app = builder.Build();

// Migraciones y usuario administrador inicial
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    context.Database.Migrate();

    if (!context.Usuarios.Any())
    {
        context.Usuarios.Add(new Usuario
        {
            PrimerNombre = "Guillermo",
            SegundoNombre = null,
            PrimerApellido = "Bixcul",
            SegundoApellido = null,
            Username = "guille117",
            Password = BCrypt.Net.BCrypt.HashPassword("Reach117"),
            Rol = Rol.Administrador,
            Activo = true
        });

        context.SaveChanges();
    }
}

// Swagger / Scalar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Evita warning de ngrok
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("ngrok-skip-browser-warning", "true");
    await next();
});

// app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();