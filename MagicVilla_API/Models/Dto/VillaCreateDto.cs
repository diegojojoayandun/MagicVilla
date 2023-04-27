using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto
{
    public class VillaCreateDto
    {
        
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; } = null!;

        public string Detalle { get; set; } = null!;
        [Required]
        public double Tarifa { get; set; }

        public int Ocupantes { get; set; }

        public int MetrosCuadrados { get; set;}
        public string ImagenUrl { get; set; } = null!;
        public string Amenidad { get; set; } = null!;
    }
}
