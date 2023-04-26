using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MagicVilla_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

            
        }
        #pragma warning restore CS8618
        public DbSet<Villa> Villas{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre = "Villa Real",
                    Detalle = "Detalle de la villa ...",
                    ImagenUrl = "",
                    Ocupantes=5,
                    MetrosCuadrados=50,
                    Tarifa=200,
                    Amenidad="",
                    FechaCreacíon= DateTime.Now,
                    FechaActualizacion= DateTime.Now

                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "Premium Vista a la psicina",
                    Detalle = "Detalle de la villa ...",
                    ImagenUrl = "",
                    Ocupantes = 6,
                    MetrosCuadrados = 80,
                    Tarifa = 300,
                    Amenidad = "",
                    FechaCreacíon = DateTime.Now,
                    FechaActualizacion = DateTime.Now

                });
        }
    }
}
