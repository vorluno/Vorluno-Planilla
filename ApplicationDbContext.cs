using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // <--- USANDO AÑADIDO
using Microsoft.EntityFrameworkCore;
using Planilla.Domain.Entities;                         // <--- USANDO AÑADIDO

namespace Planilla.Infrastructure.Data
{
    // CAMBIO CLAVE: Heredamos de IdentityDbContext<AppUser> en lugar de solo DbContext
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<ReciboDeSueldo> RecibosDeSueldo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esta línea es crucial al heredar de IdentityDbContext
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.NumeroIdentificacion)
                .IsUnique();
        }
    }
}