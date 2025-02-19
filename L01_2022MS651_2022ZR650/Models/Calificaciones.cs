using System.ComponentModel.DataAnnotations;

namespace L01_2022MS651_2022ZR650.Models
{
    public class Calificaciones
    {
        [Key]
        public int calificacionId { get; set; }
        public int? publicacionId { get; set; }

        public int? usuarioId { get; set; }
        public int? calificacion { get; set; }
    }
}
