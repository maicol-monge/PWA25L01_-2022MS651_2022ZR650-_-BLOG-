using System.ComponentModel.DataAnnotations;

namespace L01_2022MS651_2022ZR650.Models
{
    public class Usuarios
    {
        [Key]
        public int usuarioId { get; set; }
        public int? rolId { get; set; }
        public string nombreUsuario { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
    }
}
