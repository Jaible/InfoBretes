// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using CasoPracticoWeb.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace WebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Archivo> Archivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Solicitud>().ToTable("Solicitud", "dbo");
            modelBuilder.Entity<Archivo>().ToTable("Archivos", "dbo");

            modelBuilder.Entity<Solicitud>()
                .HasMany(s => s.Archivos)
                .WithOne(a => a.Solicitud)
                .HasForeignKey(a => a.IdSolicitud);
        }
    }
}
