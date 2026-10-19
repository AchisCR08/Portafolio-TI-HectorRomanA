using Microsoft.EntityFrameworkCore;
using Taller_Todo_Terreno.Models;

namespace Taller_Todo_Terreno.Data
{
    public class TallerContext: DbContext
    {
        public TallerContext(DbContextOptions<TallerContext> options) : base(options)
        {
        }

        public DbSet<Dueno> Duenos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
    }
}
