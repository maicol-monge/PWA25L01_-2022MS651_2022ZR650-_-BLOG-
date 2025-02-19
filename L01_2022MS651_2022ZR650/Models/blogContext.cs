using Microsoft.EntityFrameworkCore;

namespace L01_2022MS651_2022ZR650.Models
{
    public class blogContext: DbContext
    {
        public blogContext(DbContextOptions<blogContext> options) : base(options)
        {
        }

        public DbSet<Calificaciones> calificaciones { get; set; }
        public DbSet<Comentarios> comentarios { get; set; }
        public DbSet<Roles> roles { get; set; }
    }
}
