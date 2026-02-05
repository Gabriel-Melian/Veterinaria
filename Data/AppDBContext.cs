using Microsoft.EntityFrameworkCore;
using Veterinaria.Models;

namespace Veterinaria.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<Calendario> Calendarios { get; set; }
        public DbSet<Img> Imgs { get; set; }
        public DbSet<Turno> Turnos { get; set; }
    }
}

/*
Lo siguiente me generaria las tablas con sus respectivos campos en la BDD
IMPORTANTE: Hacerlo con Laragon corriendo y la base de datos levantada!!!!

dotnet tool install --global dotnet-ef   -> Solo la primera vez para instalar Entity Framework

Despues esto, para instalar el paquete de diseño y poder usar comandos:
dotnet add package Microsoft.EntityFrameworkCore.Design

Despues, ejecutar:
dotnet ef migrations add InitialCreate
dotnet ef database update

Explicacion basica:
Microsoft.EntityFrameworkCore → EF Core básico (runtime).
Pomelo.EntityFrameworkCore.MySql → el proveedor MySQL.
Microsoft.EntityFrameworkCore.Design → herramientas que permiten a EF generar código de migración y crear tablas.

Cada vez que haga un cambio en cuanto a estructura (agregar campo, eliminar tabla, etc.), debo ejecutar:
dotnet ef migrations add NOMBRE_DE_LA_MIGRACION
dotnet ef database update
*/