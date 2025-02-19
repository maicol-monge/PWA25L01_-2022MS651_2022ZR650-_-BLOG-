using System.ComponentModel.DataAnnotations;

namespace L01_2022MS651_2022ZR650.Models
{
    public class Roles
    {
        [Key]
        public int rolId { get; set; }
        public string? rol { get; set; }


    }
}
