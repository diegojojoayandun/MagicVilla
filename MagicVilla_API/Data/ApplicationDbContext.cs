﻿using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
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
