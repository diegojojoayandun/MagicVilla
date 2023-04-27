﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; } = null!;

        public string Detalle { get; set; } = null!;
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set;}
        [Required]
        public string ImagenUrl { get; set; } = null!;
        public string Amenidad { get; set; } = null!;
    }
}